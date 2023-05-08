using BusinessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreEmlakApp.Controllers
{
    public class AdvertController : Controller
    {
        private readonly ImagesService _imagesService;
        private readonly AdvertService _advertService;
        private readonly CityService _cityService;
        private readonly DistrictService _districtService;
        private readonly NeighbourhoodService _neighbourhoodService;
        private readonly SituationService _situationService;
        private readonly TypeService _typeService;
        private readonly FrontService _frontService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly CategoryService _categoryService;

        public AdvertController(ImagesService imagesService,AdvertService advertService,CityService cityService, DistrictService districtService, NeighbourhoodService neighbourhoodService,
            SituationService situationService, CategoryService categoryService ,TypeService typeService,FrontService frontService, FuelTypeService fuelTypeService)
        {
            this._imagesService = imagesService;
            this._advertService = advertService;
            this._cityService = cityService;
            this._districtService = districtService;
            this._neighbourhoodService = neighbourhoodService;
            this._situationService = situationService;
            this._typeService = typeService;
            this._categoryService = categoryService;
            this._frontService = frontService;
            this._fuelTypeService = fuelTypeService;

        }

       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PropertyDetails(int id)
        {
            var details = _advertService.TGetById(id);
            ViewBag.images = _imagesService.List(x => x.AdvertId == id);
            return View(details);
        }
        public IActionResult AdvertAll()
        {
            Dropdown();
            var list = _advertService.List(x => x.Status == true);
            var images = _imagesService.List(x => x.Status == true);
            ViewBag.images = images;
            return View(list);
        }
        public IActionResult AdvertRent()
        {
            Dropdown();
            var rent = _advertService.List(x => x.Situation.SituationName == "Rent");
            ViewBag.count = rent.Count;
            ViewBag.images = _imagesService.List(x => x.Status==true);
            return View(rent);
        }

        public IActionResult AdvertSale()
        {
            Dropdown();
            var sale = _advertService.List(x => x.Situation.SituationName == "Sale");
            ViewBag.count = sale.Count;
            ViewBag.images = _imagesService.List(x => x.Status==true);
            return View(sale);
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
            List<City> cityList = _cityService.List(x => x.Status == true);
            return cityList;
        }
        public List<Situation> SituationGet()
        {
            List<Situation> situationList = _situationService.List(x => x.Status == true);
            return situationList;
        }
        public IActionResult DistrictGet(int CityId)
        {
            List<District> districtList = _districtService.List(x => x.Status == true && x.CityId == CityId);
            ViewBag.district = new SelectList(districtList, "DistrictId", "DistictName");
            return PartialView("DistrictPartial");
        }
        public IActionResult TypeGet()
        {
            List<EntityLayer.Entities.Type> typeList = _typeService.List(x => x.Status == true);
            ViewBag.type = new SelectList(typeList, "TypeId", "TypeName");
            return PartialView("TypePartial");
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            List<Neighbourhood> neighbourhoodList = _neighbourhoodService.List(x => x.Status == true && x.DistrictId == DistrictId);
            ViewBag.neighbour = new SelectList(neighbourhoodList, "NeighbourhoodId", "NeighbourhoodName");
            return PartialView("NeighbourhoodPartial");
        }
        public IActionResult Filter(int min, int max, int cityId, int typeId, int neighbourhoodId, int situationId, string searchString)
        {

            Dropdown();
            ViewData["CurrentFilter"] = searchString;
            var imageList = _imagesService.List(x => x.Status == true);
            ViewBag.imageList = imageList;
            var filter = _advertService.List(x => x.Price >= min && x.Price <= max && x.CityId == cityId && x.TypeId == typeId && x.SituationId == situationId && x.NeighbourhoodId == neighbourhoodId);
            if (!String.IsNullOrEmpty(searchString))
            {
               _advertService.List(x=>x.Status==true).Where(s => s.AdvertTitle.Contains(searchString)
                                       || s.Description.Contains(searchString) || s.Type.TypeName.Contains(searchString) || s.Situation.SituationName.Contains(searchString));
            }

            return View(filter);


        }
        public PartialViewResult PartialFilter()
        {
            Dropdown();
            return PartialView();
        }
        public void Dropdown()
        {
            // ViewBag.cityList = new SelectList(CityGet(), "CityId", "CityName");
            List<SelectListItem> value = (from x in _cityService.List(x => x.Status == true)
                                          select new SelectListItem
                                          {
                                              Text = x.CityName,
                                              Value = x.CityId.ToString()


                                          }).ToList();

            ViewBag.cityList = value;
            // ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");
            List<SelectListItem> situationList = (from x in _situationService.List(x => x.Status == true)
                                                  select new SelectListItem
                                                  {
                                                      Text = x.SituationName,
                                                      Value = x.SituationId.ToString()


                                                  }).ToList();

            ViewBag.situations = situationList;

            List<SelectListItem> category = (from x in _categoryService.List(x => x.Status == true)
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryId.ToString()


                                             }).ToList();

            ViewBag.category = category;

            List<SelectListItem> value1 = (from x in _districtService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.DistrictName,
                                               Value = x.DistrictId.ToString()


                                           }).ToList();

            ViewBag.district = value1;

            List<SelectListItem> value2 = (from x in _neighbourhoodService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.NeighbourhoodName,
                                               Value = x.NeighbourhoodId.ToString()


                                           }).ToList();

            ViewBag.neighbourhood = value2;
            List<SelectListItem> value3 = (from x in _typeService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.TypeName,
                                               Value = x.TypeId.ToString()


                                           }).ToList();

            ViewBag.type = value3;

            List<SelectListItem> value4 = (from x in _situationService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.SituationName,
                                               Value = x.SituationId.ToString()


                                           }).ToList();

            ViewBag.situation = value4;

            List<SelectListItem> value5 = (from x in _fuelTypeService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.FuelTypeName,
                                               Value = x.FuelTypeId.ToString()


                                           }).ToList();

            ViewBag.FuelTypes = value5;

            List<SelectListItem> value6 = (from x in _frontService.List(x => x.Status == true)
                                           select new SelectListItem
                                           {
                                               Text = x.FrontName,
                                               Value = x.FrontId.ToString()


                                           }).ToList();

            ViewBag.Fronts = value6;


        }
    }
}
