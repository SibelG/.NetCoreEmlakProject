using BusinessLayer.Abstract;
using EntityLayer.Entities;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
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
        public IActionResult Filter(String currentFilter, int? min, int? max, int? CityId, int? CategoryId, int? TypeId, int? NeighbourhoodId, int? SituationId, string searchString,int? pageNo)
        {

            Dropdown();
            if (searchString != null)
            {
                pageNo = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
       

            var imageList = _projectImageService.List(x => x.Status == true);
            ViewBag.images = imageList;
            var filter=_projectService.List(x=>x.Status == true);
            if (min != null)
            {
                filter.Where(x => x.Price >= min);
            }
            if (max != null)
            {
                filter.Where(x => x.Price <= max);
            }
            if (CityId != null)
            {
                filter.Where(x => x.CityId == CityId);
            }
            if (CategoryId != null)
            {
                filter.Where(x => x.CategoryId == CategoryId);
            }
            if (TypeId != null)
            {
                filter.Where(x => x.TypeId == TypeId);
            }
            if (SituationId != null)
            {
                filter.Where(x => x.SituationId == SituationId);
            }
            if (NeighbourhoodId != null)
            {
                filter.Where(x => x.NeighbourhoodId == NeighbourhoodId);
            }

            //var filter = _projectService.List(x => x.Price >= _min && x.Price <= _max && x.CityId == _cityId && x.TypeId == _typeId && x.SituationId == _situationId && x.NeighbourhoodId == _neighbourhoodId);
            if (!String.IsNullOrEmpty(searchString))
            {
                filter.Where(s => s.ProjectTitle.Contains(searchString)
                                        || s.Description.Contains(searchString) || s.Type.TypeName.Contains(searchString) || s.Situation.SituationName.Contains(searchString));
            }
            int pageSize = 6;
            int _pageNo = (pageNo ?? 1);
            return View(filter.ToPagedList<Projects>(_pageNo, pageSize));


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
