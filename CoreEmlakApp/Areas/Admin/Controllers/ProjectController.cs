using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {

        private readonly CityService cityService;
        private readonly DistrictService districtService;
        private readonly NeighbourhoodService neighbourhoodService;
        private readonly SituationService situationService;
        private readonly TypeService typeService;
        private readonly ProjectService projectService;
        private readonly ProjectImageService projectImageService;
        private readonly HeadingService headingService;
        private readonly CategoryService categoryService;

        private readonly IWebHostEnvironment webHostEnvironment;



        public ProjectController(CityService cityService, DistrictService districtService,
            NeighbourhoodService neighbourhoodService, SituationService situationService, TypeService typeService,
            IWebHostEnvironment webHostEnvironment, CategoryService categoryService, HeadingService headingService, ProjectService projectService, ProjectImageService projectImageService, ImagesService imagesService, FuelTypeService fuelTypeService, FrontService frontService)
        {
            this.cityService = cityService;
            this.districtService = districtService;
            this.neighbourhoodService = neighbourhoodService;
            this.situationService = situationService;
            this.typeService = typeService;
            this.categoryService = categoryService;
            this.projectService = projectService;
            this.webHostEnvironment = webHostEnvironment;
            this.projectImageService = projectImageService; 
            this.categoryService = categoryService;
            this.headingService = headingService;

        }


        public IActionResult Index()
        {
            string id = HttpContext.Session.GetString("Id");
            var list = projectService.List(x => x.Status == true);
            //var list = advertService.List(x => x.Status == true);
            return View(list);
        }

        public IActionResult ImageList(int id)
        {
            var list = projectImageService.List(x => x.Status == true && x.ProjectId == id);
            return View(list);
            //return View();
        }

        public IActionResult ImageCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageCreate(Projects data)
        {
            var project = projectService.TGetById(data.ProjectId);
            if (data.Image != null)
            {
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "imgProject");
                foreach (var item in data.Image)
                {
                    var fullFilePath = Path.Combine(filePath, item.FileName);

                    using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                    {
                        item.CopyTo(dosyaAkısı);
                    }

                    projectImageService.TAdd(new ProjectImage { ImageName = item.FileName, Status = true, ProjectId = data.ProjectId });
                }


                TempData["Success"] = "Project Success";
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public IActionResult ImageDelete(int id)
        {
            var delete = projectImageService.TGetById(id);
            projectImageService.TDelete(delete);
            return RedirectToAction("Index");

        }

        public IActionResult ImageUpdate(int id)
        {
            var image = projectImageService.TGetById(id);
            return View(image);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ImageUpdate(ProjectImage data)
        {
            var image = projectImageService.TGetById(data.ProjectImageId);
            ProjectImagesValidator validationRules = new ProjectImagesValidator();
            ValidationResult result = validationRules.Validate(image);

            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "imgProject");

                    var fullFilePath = Path.Combine(filePath, data.Image.FileName);

                    using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                    {
                        data.Image.CopyTo(dosyaAkısı);
                    }

                    projectImageService.TUpdate(data);

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

        public IActionResult Create(Projects data)
        {
            ProjectValidation validationRules = new ProjectValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                if (data.Image != null)
                {
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "imgProject");
                    foreach (var item in data.Image)
                    {
                        var fullFilePath = Path.Combine(filePath, item.FileName);

                        using (var dosyaAkısı = new FileStream(fullFilePath, FileMode.Create))
                        {
                            item.CopyTo(dosyaAkısı);
                        }

                        data.Images.Add(new ProjectImage { ImageName = item.FileName, Status = true });
                    }

                    projectService.TAdd(data);
                    TempData["Success"] = "Project Success";
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
           
            var list = projectService.List(x => x.Status == false);
            return View(list);
        }

        public IActionResult RestoreDeleted(int id)
        {
            //var SessionUser = HttpContext.Session.GetString("Id");
            var delete = projectService.TGetById(id);

            projectService.RestoreDelete(delete);
            TempData["RestoreDelete"] = "Project Again Uploaded";
            return RedirectToAction("Index");
            //return View();
          

        }
        public IActionResult Update(int id)
        {
            ViewBag.userId = HttpContext.Session.GetString("Id");
            Dropdown();
            var advert = projectService.TGetById(id);
            return View(advert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Update(Projects data)
        {
            ProjectValidation validationRules = new ProjectValidation();
            ValidationResult result = validationRules.Validate(data);

            if (result.IsValid)
            {
                projectService.TUpdate(data);
                TempData["Update"] = "Project Update Success";
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
        public IActionResult DistrictGet(int CityId)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var districtList = districtService.List(x => x.Status == true && x.CityId == CityId);
            var jsonValues = JsonConvert.SerializeObject(districtList, Formatting.Indented, settings);
            return Json(jsonValues);
        }
        public IActionResult TypeGet(int CategoryId)
        {

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var typeList = typeService.List(x => x.Status == true && x.CategoryId == CategoryId);
            var jsonValues = JsonConvert.SerializeObject(typeList, Formatting.Indented, settings);
            return Json(jsonValues);
        }
        public IActionResult NeighbourhoodGet(int DistrictId)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var neighbourList = neighbourhoodService.List(x => x.Status == true && x.DistrictId == DistrictId);
            var jsonValues = JsonConvert.SerializeObject(neighbourList, Formatting.Indented, settings);
            return Json(jsonValues);
        }
        public IActionResult FullDelete(int id)
        {
            //var SessionUser = HttpContext.Session.GetString("id");
            var delete = projectService.TGetById(id);
            projectService.FullDelete(delete);
            return RedirectToAction("Index");
         

        }
        public IActionResult Delete(int id)
        {
            //var SessionUser = HttpContext.Session.GetString("Id");
            var delete = projectService.TGetById(id);
            projectService.TDelete(delete);
            return RedirectToAction("Index");
        

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

            ViewBag.situations = situationList;

            List<SelectListItem> category = (from x in categoryService.List(x => x.Status == true)
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.CategoryId.ToString()


                                          }).ToList();

            ViewBag.category = category;
            // ViewBag.situations = new SelectList(SituationGet(), "SituationId", "SituationName");
            List<SelectListItem> heading = (from x in headingService.List(x => x.Status == true)
                                                  select new SelectListItem
                                                  {
                                                      Text = x.HeadingName,
                                                      Value = x.HeadingId.ToString()


                                                  }).ToList();

            ViewBag.heading = heading;

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

       


        }
    }
}
