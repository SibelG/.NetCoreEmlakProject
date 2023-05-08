using BusinessLayer.Abstract;
using EntityLayer.Entities;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace CoreEmlakApp.Controllers
{
    public class ProjectController : Controller
    {
       
        private readonly ProjectService _projectService;
        private readonly ProjectImageService _projectImageService;
        private readonly CityService _cityService;
        private readonly DistrictService _districtService;
        private readonly NeighbourhoodService _neighbourhoodService;
        private readonly SituationService _situationService;
        private readonly TypeService _typeService;
        private readonly FrontService _frontService;
        private readonly FuelTypeService _fuelTypeService;
        private readonly CategoryService _categoryService;

        public ProjectController(ProjectImageService projectImageService, ProjectService projectService, CityService cityService, DistrictService districtService, NeighbourhoodService neighbourhoodService,
            SituationService situationService, CategoryService categoryService, TypeService typeService, FrontService frontService, FuelTypeService fuelTypeService)
        {
            this._projectService = projectService;
            this._projectImageService = projectImageService;
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
            var details = _projectService.TGetById(id);
            ViewBag.images = _projectImageService.List(x => x.ProjectId == id);
            return View(details);
        }

        public IActionResult ProjectAll(int? pageNo)
        {
            Dropdown();
            int _pageNo = pageNo ?? 1;
            var list = _projectService.List(x => x.Status == true).ToPagedList<Projects>(_pageNo,6);
            var images = _projectImageService.List(x => x.Status == true);
            ViewBag.images = images;
            return View(list);
        }
        public IActionResult Filter(int min, int max, int cityId, int typeId, int neighbourhoodId, int situationId, string searchString)
        {

            Dropdown();
            ViewData["CurrentFilter"] = searchString;
            var imageList = _projectImageService.List(x => x.Status == true);
            ViewBag.imageList = imageList;
            var filter = _projectService.List(x => x.Price >= min && x.Price <= max && x.CityId == cityId && x.TypeId == typeId && x.SituationId == situationId && x.NeighbourhoodId == neighbourhoodId);
            if (!String.IsNullOrEmpty(searchString))
            {
                _projectService.List(x => x.Status == true).Where(s => s.ProjectTitle.Contains(searchString)
                                        || s.Description.Contains(searchString) || s.Type.TypeName.Contains(searchString) || s.Situation.SituationName.Contains(searchString));
            }

            return View(filter);


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
