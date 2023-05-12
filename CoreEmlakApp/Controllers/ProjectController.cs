using BusinessLayer.Abstract;
using DataAccessLayer.Migrations;
using EntityLayer.Entities;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Printing;
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
            var count = _projectService.List(x=>x.Status==true).Count();
            ViewBag.count = count;
            return View(list);
        }

        public IActionResult Filter(String currentFilter, string searchString, int? min, int? max, int? CityId, int? CategoryId, int? TypeId, int? NeighbourhoodId, int? SituationId, int? pageNo)
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
            var filter = _projectService.List(x => x.Price >= min && x.Price <= max && x.CityId == CityId && x.TypeId == TypeId && x.SituationId == SituationId && x.NeighbourhoodId == NeighbourhoodId);
            if (!String.IsNullOrEmpty(searchString))
            {
                filter.Where(x => x.ProjectTitle.Contains(searchString)
                                        || x.Description.Contains(searchString) || x.Type.TypeName.Contains(searchString) || x.Situation.SituationName.Contains(searchString));
            }
            int _pageNo = pageNo ?? 1;
            int pageSize = 6;
            return View(filter.ToPagedList<Projects>(_pageNo, pageSize));
        }

        
        //public IActionResult Filter(String currentFilter, int? min, int? max, int? CityId, int? CategoryId, int? TypeId, int? NeighbourhoodId, int? SituationId, string searchString, int? pageNo)
        //{

        //    Dropdown();
        //    if (searchString != null)
        //    {
        //        pageNo = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewData["CurrentFilter"] = searchString;


        //    var imageList = _projectImageService.List(x => x.Status == true);
        //    ViewBag.images = imageList;
        //    var filter = from x in _projectService.List(x => x.Status == true) select x;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        filter.Where(x => x.ProjectTitle.Contains(searchString)
        //                                || x.Description.Contains(searchString) || x.Type.TypeName.Contains(searchString) || x.Situation.SituationName.Contains(searchString));
        //    }

        //    if (min != null)
        //    {
        //        filter.Where(x => x.Price >= min);
        //    }
        //    if (max != null)
        //    {
        //        filter.Where(x => x.Price <= max);
        //    }
        //    if (CityId != null)
        //    {
        //        filter.Where(x => x.CityId == CityId);
        //    }
        //    if (CategoryId != null)
        //    {
        //        filter.Where(x => x.CategoryId == CategoryId);
        //    }
        //    if (TypeId != null)
        //    {
        //        filter.Where(x => x.TypeId == TypeId);
        //    }
        //    if (SituationId != null)
        //    {
        //        filter.Where(x => x.SituationId == SituationId);
        //    }
        //    if (NeighbourhoodId != null)
        //    {
        //        filter.Where(x => x.NeighbourhoodId == NeighbourhoodId);
        //    }
        //    var count = _projectService.List(x => x.Status == true).Count();
        //    ViewBag.count = count;
        //    //var filter = _projectService.List(x => x.Price >= _min && x.Price <= _max && x.CityId == _cityId && x.TypeId == _typeId && x.SituationId == _situationId && x.NeighbourhoodId == _neighbourhoodId);

        //    int pageSize = 6;
        //    int _pageNo = (pageNo ?? 1);
        //    return View(filter.ToPagedList<Projects>(_pageNo, pageSize));


        //}
        public IActionResult Sorting(string sortOrder, int? pageNo)
        {
            Dropdown();
            var images = _projectImageService.List(x => x.Status == true);
            ViewBag.images = images;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price_desc" ? "" : "price_desc";
            ViewData["PriceSortAscParm"] = sortOrder == "price_asc" ? "" : "price_asc";
     
            var projects = from s in _projectService.List(x => x.Status == true)
                           select s;
            switch (sortOrder)
            {
                case "date_desc":
                    projects = projects.OrderByDescending(s => s.DeliveryDate);
                    break;
                case "price_asc":
                    projects = projects.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    projects = projects.OrderByDescending(s => s.Price);
                    break;
                default:
                    projects = projects.OrderBy(s => s.DeliveryDate);
                    break;
            }
            var count = _projectService.List(x => x.Status == true).Count();
            ViewBag.count = count;
            int pageSize = 6;
            int _pageNo = (pageNo ?? 1);
            return View(projects.ToList().ToPagedList<Projects>(_pageNo, pageSize));
          
        }
        public PartialViewResult PartialFilter()
        {
            Dropdown();
            return PartialView();
        }
        public PartialViewResult PartialSorter(String sortOrder, int? pageNo)
        {
            Dropdown();
            var images = _projectImageService.List(x => x.Status == true);
            ViewBag.images = images;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price_desc" ? "" : "price_desc";
            ViewData["PriceSortAscParm"] = sortOrder == "price_asc" ? "" : "price_asc";

            var projects = from s in _projectService.List(x => x.Status == true)
                           select s;
            switch (sortOrder)
            {
                case "date_desc":
                    projects = projects.OrderByDescending(s => s.DeliveryDate);
                    break;
                case "price_asc":
                    projects = projects.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    projects = projects.OrderByDescending(s => s.Price);
                    break;
                default:
                    projects = projects.OrderBy(s => s.DeliveryDate);
                    break;
            }
            int pageSize = 6;
            int _pageNo = (pageNo ?? 1);
            return PartialView(projects.ToList().ToPagedList<Projects>(_pageNo, pageSize));
        }
        public IActionResult DistrictGet(int CityId)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var districtList = _districtService.List(x => x.Status == true && x.CityId == CityId);
            var jsonValues = JsonConvert.SerializeObject(districtList, Formatting.Indented, settings);
            return Json(jsonValues);
        }
        public IActionResult TypeGet(int CategoryId)
        {

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var typeList = _typeService.List(x => x.Status == true && x.CategoryId == CategoryId);
            var jsonValues = JsonConvert.SerializeObject(typeList, Formatting.Indented, settings);
            return Json(jsonValues);
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var neighbourList = _neighbourhoodService.List(x => x.Status == true && x.DistrictId == DistrictId);
            var jsonValues = JsonConvert.SerializeObject(neighbourList, Formatting.Indented, settings);
            return Json(jsonValues);
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
