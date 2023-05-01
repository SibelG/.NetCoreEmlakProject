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
    [Authorize(Roles="Admin")]
    public class DistrictController : Controller
    {
        DistrictService districtService;
        CityService cityService;


        public DistrictController(DistrictService districtService,CityService cityService)
        {
            this.districtService = districtService;
            this.cityService = cityService;
        }

        public IActionResult Index()
        {
            var district = districtService.List(x => x.Status == true);
            return View(district);
        }

        public IActionResult Create()
        {
            Dropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(District data)
        {

            DistrictValidator validationRules = new DistrictValidator();
            ValidationResult result = validationRules.Validate(data);




            if (result.IsValid)
            {
                districtService.TAdd(data);
                TempData["Success"] = "District Added Success";
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

        public IActionResult Delete(int id) { 
            var district = districtService.TGetById(id);
            districtService.TDelete(district);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Dropdown();
            var update = districtService.TGetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(District data)
        {


            DistrictValidator validationRules = new DistrictValidator();
            ValidationResult result = validationRules.Validate(data);



            if (result.IsValid)
            {

                districtService.TUpdate(data);
                TempData["Update"] = "District Update Success";
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
            return View(data);
        }
        public void Dropdown()
        {
            List<SelectListItem> value = (from x in cityService.List(x => x.Status == true)
                                          select new SelectListItem
                                          {
                                              Text = x.CityName,
                                              Value = x.CityId.ToString(),

                                          }).ToList();

            ViewBag.cities = value;

        }
    }
}
