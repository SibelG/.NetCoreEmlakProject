using BusinessLayer.Abstract;
using BusinessLayer.ValidadionRules;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CoreEmlakApp.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles="User")]
    public class AdvertController : Controller
    {
        AdvertService advertService;
        CityService cityService;
        DistrictService districtService;
        NeighbourhoodService neighbourhoodService;
        SituationService situationService;
        TypeService typeService;
        ImagesService imagesService;
        CategoryService categoryService;
        FuelTypeService fuelTypeService;
        FrontService frontService;

        IWebHostEnvironment webHostEnvironment;



        public AdvertController(AdvertService advertService, CityService cityService, DistrictService districtService,FuelTypeService fuelTypeService, FrontService frontService, NeighbourhoodService neighbourhoodService,CategoryService categoryService, SituationService situationService, TypeService typeService, IWebHostEnvironment webHostEnvironment, ImagesService imagesService)
        {
            this.advertService = advertService;
            this.cityService = cityService;
            this.districtService = districtService;
            this.neighbourhoodService = neighbourhoodService;
            this.situationService = situationService;
            this.typeService = typeService;
            this.categoryService = categoryService;
            this.fuelTypeService = fuelTypeService;
            this.frontService = frontService;
            this.imagesService = imagesService;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("id");
            var list = advertService.List(x => x.Status == true && x.UserAdminId == id);
            //var list = advertService.List(x => x.Status == true);
            return View(list);
        }

        public IActionResult Create()
        {
            ViewBag.userId = HttpContext.Session.GetString("id");
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


            string id = HttpContext.Session.GetString("Id");

            if (result.IsValid)
            {
                if (data.Image != null && data.UserAdminId == id)
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
            List<City> cityList = cityService.List(x => x.Status == true);
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
        public JsonResult TypeGet(int CategoryId)
        {
            List<EntityLayer.Entities.Type> typeList = typeService.List(x => x.Status == true && x.CategoryId == CategoryId);
            ViewBag.type = new SelectList(typeList, "TypeId", "TypeName");
            return Json(new SelectList(typeList, "TypeId", "TypeName"));
            //return PartialView("TypePartial");
        
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            List<Neighbourhood> neighbourhoodList = neighbourhoodService.List(x => x.Status == true && x.DistrictId == DistrictId);
            ViewBag.neighbour = new SelectList(neighbourhoodList, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
        }
        public IActionResult Filter(int min, int max, int cityId, int typeId,int neighbourhoodId, int situationId)
        {
            Dropdown();
            var imageList = imagesService.List(x=>x.Status== true);
            ViewBag.imageList = imageList;
            var filter = advertService.List(x => x.Price >= min && x.Price <= max && x.CityId == cityId && x.TypeId == typeId && x.SituationId == situationId && x.NeighbourhoodId == neighbourhoodId);
            return View(filter);


        }
        public PartialViewResult PartialFilter()
        {
            Dropdown();
            return PartialView();
        }

        public void Dropdown()
        {
            ViewBag.cityList = new SelectList(CityGet(), "CityId", "CityName");
            ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");


            List<SelectListItem> value1 = (from x in districtService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.DistrictName,
                                               Value = x.DistrictId.ToString()


                                           }).ToList();

            ViewBag.district = value1;
            List<SelectListItem> category = (from x in categoryService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()


                                           }).ToList();

            ViewBag.category = category;

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
