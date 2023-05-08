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
    [Authorize(Roles ="Admin")]
    public class AdvertController : Controller
    {
        private readonly AdvertService advertService;
        private readonly CityService cityService;
        private readonly DistrictService districtService;
        private readonly NeighbourhoodService neighbourhoodService;
        private readonly SituationService situationService;
        private readonly TypeService typeService;
        private readonly ImagesService imagesService;
        private readonly FrontService frontService;
        private readonly FuelTypeService fuelTypeService;

        private readonly IWebHostEnvironment webHostEnvironment;
      

      
        public AdvertController(AdvertService advertService,CityService cityService,DistrictService districtService,
            NeighbourhoodService neighbourhoodService,SituationService situationService,TypeService typeService,
            IWebHostEnvironment webHostEnvironment,ImagesService imagesService,FuelTypeService fuelTypeService,FrontService frontService)
        {
            this.advertService = advertService;
            this.cityService = cityService;
            this.districtService = districtService;
            this.neighbourhoodService = neighbourhoodService;
            this.situationService = situationService;
            this.typeService = typeService; 
            this.imagesService = imagesService;
            this.fuelTypeService = fuelTypeService;
            this.frontService = frontService;
            this.webHostEnvironment = webHostEnvironment;
            
        }

        
        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("Id");
            var list = advertService.List(x => x.Status == true && x.UserAdminId==id);
            //var list = advertService.List(x => x.Status == true);
            return View(list);
        }

        public IActionResult ImageList(int id)
        {
            var list = imagesService.List(x => x.Status == true && x.AdvertId == id);
            return View(list);
        }

        public IActionResult ImageCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageCreate(Advert data)
        {
            var advert = advertService.TGetById(data.AdvertId);
            if (data.Image != null)
            {
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "img");
                foreach (var item in data.Image)
                {
                    var fullFilePath = Path.Combine(filePath, item.FileName);

                    using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                    {
                        item.CopyTo(dosyaAkısı);
                    }

                   imagesService.TAdd(new Images { ImageName = item.FileName, Status = true ,AdvertId = data.AdvertId});
                }

               
                TempData["Success"] = "Advert Image Success";
                return RedirectToAction("Index");
            }
            return View(advert);
        }

        public IActionResult ImageDelete(int id)
        {
            var delete = imagesService.TGetById(id);
            imagesService.TDelete(delete);  
            return RedirectToAction("Index");

        }

        public IActionResult ImageUpdate(int id)
        {
            var image = imagesService.TGetById(id);
            return View(image);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageUpdate(Images data)
        {
            var image = imagesService.TGetById(data.ImageId);


            ImagesValidator validationRules = new ImagesValidator();
            ValidationResult result = validationRules.Validate(image);

            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "img");
                 
                    var fullFilePath = Path.Combine(filePath, data.Image.FileName);

                        using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                        {
                            data.Image.CopyTo(dosyaAkısı);
                        }

                       
                    

                    imagesService.TUpdate(data);
                   
                    return RedirectToAction("Index");
                }

            }
            else
            {
                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        public IActionResult Create()
        {
            ViewBag.userId = HttpContext.Session.GetString("Id");
            Dropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Advert data)
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "img");
                    foreach (var item in data.Image)
                    {
                        var fullFilePath = Path.Combine(filePath, item.FileName);

                        using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                        {
                            item.CopyTo(dosyaAkısı);
                        }

                        data.Images.Add(new Images { ImageName = item.FileName, Status = true });
                    }

                    advertService.TAdd(data);
                    TempData["Success"] = "Advert Success";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            Dropdown();
            return View();
        }

        public IActionResult DeleteList()
        {
            string id = HttpContext.Session.GetString("Id");
            var list = advertService.List(x => x.Status == false && x.UserAdminId == id);
            return View(list);
        }

        public IActionResult RestoreDeleted(int id)
        {
            var SessionUser = HttpContext.Session.GetString("Id");
            var delete = advertService.TGetById(id);

            if (SessionUser.ToString() == delete.UserAdminId)
            {
                advertService.RestoreDelete(delete);
                TempData["RestoreDelete"] = "Advert Again Uploaded";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Update(int id)
        {
            ViewBag.userId = HttpContext.Session.GetString("Id");
            Dropdown();
            var advert = advertService.TGetById(id);
            return View(advert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Update(Advert data)
        {
            AdvertValidation validationRules = new AdvertValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                advertService.TUpdate(data);
                TempData["Update"] = "Advert Update Success";
                return RedirectToAction("Index");
               

            }
            else
            {
                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            Dropdown();
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
            var SessionUser = HttpContext.Session.GetString("Id");
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
        public IActionResult TypeGet(int CategoryId)
        {
            List<EntityLayer.Entities.Type> typeList = typeService.List(x => x.Status == true && x.CategoryId== CategoryId);
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
            // ViewBag.cityList = new SelectList(CityGet(), "CityId", "CityName");
            List<SelectListItem> value = (from x in cityService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.CityName,
                                               Value = x.CityId.ToString()


                                           }).ToList();

            ViewBag.cityList = value;
           // ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");
            List<SelectListItem> situationList = (from x in situationService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.SituationName,
                                               Value = x.SituationId.ToString()


                                           }).ToList();

            ViewBag.situations= situationList;

            List<SelectListItem> value1 = (from x in districtService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.DistrictName,
                                               Value = x.DistrictId.ToString()


                                           }).ToList();

            ViewBag.district = value1;

            List<SelectListItem> value2 = (from x in neighbourhoodService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.NeighbourhoodName,
                                               Value = x.NeighbourhoodId.ToString()


                                           }).ToList();

            ViewBag.neighbourhood = value2;
            List<SelectListItem> value3 = (from x in typeService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.TypeName,
                                               Value = x.TypeId.ToString()


                                           }).ToList();

            ViewBag.type = value3;

            List<SelectListItem> value4 = (from x in situationService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.SituationName,
                                               Value = x.SituationId.ToString()


                                           }).ToList();

            ViewBag.situation = value4;

            List<SelectListItem> value5 = (from x in fuelTypeService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.FuelTypeName,
                                               Value = x.FuelTypeId.ToString()


                                           }).ToList();

            ViewBag.FuelTypes = value5;

            List<SelectListItem> value6 = (from x in frontService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.FrontName,
                                               Value = x.FrontId.ToString()


                                           }).ToList();

            ViewBag.Fronts = value6;


        }
    }
}
