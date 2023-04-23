using BusinessLayer.Abstract;
using BusinessLayer.ValidadionRules;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles ="Admin")]
    public class AdvertController : Controller
    {
        AdvertService advertService;
        CityService cityService;
        DistrictService districtService;
        NeighbourhoodService neighbourhoodService;
        SituationService situationService;
        TypeService typeService;

        IWebHostEnvironment webHostEnvironment;
      

      
        public AdvertController(AdvertService advertService,CityService cityService,DistrictService districtService,NeighbourhoodService neighbourhoodService,SituationService situationService,TypeService typeService,IWebHostEnvironment webHostEnvironment)
        {
            this.advertService = advertService;
            this.cityService = cityService;
            this.districtService = districtService;
            this.neighbourhoodService = neighbourhoodService;
            this.situationService = situationService;
            this.typeService = typeService; 
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("id");
            var list = advertService.List(x => x.Status == true && x.UserAdminId==id);
            return View(list);
        }
        public IActionResult Create()
        {
            ViewBag.userId = HttpContext.Session.GetString("userId");
            Dropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Create(Advert data)
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "img");
                    foreach(var item in data.Image) { 
                        var fullFilePath = Path.Combine(filePath, item.FileName);

                        using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                        {
                            item.CopyTo(dosyaAkısı);
                        }

                        data.Images.Add(new Images { ImageName = item.FileName ,Status = true});
                    }
                    
                    advertService.TAdd(data);
                    TempData["Success"] = "Advert Success";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                foreach(var item in result.Errors) {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
           
            Dropdown();
            return View();
        }

        public IActionResult DeleteList()
        {
            string id = HttpContext.Session.GetString("id");
            var list = advertService.List(x => x.Status == true && x.UserAdminId == id);
            return View(list);
        }

        public IActionResult RestoreDeleted(int id)
        {
            var SessionUser = HttpContext.Session.GetString("id");
            var delete = advertService.TGetById(id);

            if (SessionUser.ToString() == delete.UserAdminId)
            {
                advertService.RestoreDelete(delete);
                TempData["RestoreDelete"] = "Advert Again Uploaded";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult FullDelete(int id)
        {
            var SessionUser = HttpContext.Session.GetString("id");
            var delete = advertService.TGetById(id);

            if (SessionUser.ToString() == delete.UserAdminId)
            {
                advertService.FullDelete(delete);
                
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int id)
        {
            var SessionUser = HttpContext.Session.GetString("id");
            var delete = advertService.TGetById(id);

            if (SessionUser.ToString() == delete.UserAdminId)
            {
                advertService.TDelete(delete);
                return RedirectToAction("Index");
            }
            return View();
            
        }
        public PartialViewResult DistrictPartial()
        {
            return PartialView();
        }
        public PartialViewResult TypePartial()
        {
            return PartialView();
        }
        public PartialViewResult NeighbourhoodPartial()
        {
            return PartialView();
        }

        public List<City> CityGet()
        {
            List<City> cityList = cityService.List(x=>x.Status == true);
            return cityList;
        }
        public List<Situation> SituationGet()
        {
            List<Situation> situationList = situationService.List(x => x.Status == true);
            return situationList;
        }
        public IActionResult DistrictGet(int CityId)
        {
            List<District> districtList = districtService.List(x => x.Status == true && x.CityId == CityId);
            ViewBag.district = new SelectList(districtList, "DistrictId", "DistictName");
            return PartialView("DistrictPartial");
        }
        public IActionResult TypeGet(int SituationId)
        {
            List<EntityLayer.Entities.Type> typeList = typeService.List(x => x.Status == true && x.SituationId == SituationId);
            ViewBag.type = new SelectList(typeList, "TypeId", "TypeName");
            return PartialView("TypePartial");
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            List<Neighbourhood> neighbourhoodList = neighbourhoodService.List(x => x.Status == true && x.DistrictId == DistrictId);
            ViewBag.neighbour = new SelectList(neighbourhoodList, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
        }
        public void Dropdown()
        {
            ViewBag.cityList = new SelectList(CityGet(), "CityId", "CityName");
            ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");
        }
    }
}
