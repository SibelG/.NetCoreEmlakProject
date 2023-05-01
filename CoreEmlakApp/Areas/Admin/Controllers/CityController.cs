using BusinessLayer.Abstract;
using BusinessLayer.ValidadionRules;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class CityController : Controller
    {
        CityService cityService;

        public CityController(CityService cityService)
        {
            this.cityService = cityService;
        }

        public IActionResult Index()
        {
            var list = cityService.List(x => x.Status == true);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City data)
        {

             CityValidator validationRules = new CityValidator();
             ValidationResult result = validationRules.Validate(data);



             if (result.IsValid)
              {
                  cityService.TAdd(data);
                  TempData["Success"] = "City Added Success";
                  return RedirectToAction("Index");
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
        public IActionResult Delete(int id)
        {
            var city = cityService.TGetById(id);
            cityService.TDelete(city);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var update = cityService.TGetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(City data)
        {

         
            CityValidator validationRules = new CityValidator();
            ValidationResult result = validationRules.Validate(data);



            if (result.IsValid)
            {
                
                cityService.TUpdate(data);
                TempData["Update"] = "City Update Success";
                return RedirectToAction("Index");
            }

            else
            {
                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(data);
        }

    }
}
