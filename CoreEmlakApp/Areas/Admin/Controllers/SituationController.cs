using BusinessLayer.Abstract;
using BusinessLayer.ValidadionRules;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SituationController : Controller
    {

        SituationService SituationService;

        public SituationController(SituationService SituationService)
        {
            this.SituationService = SituationService;
        }

        public IActionResult Index()
        {
            var list = SituationService.List(x => x.Status == true);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Situation data)
        {

            SituationValidator validationRules = new SituationValidator();
            ValidationResult result = validationRules.Validate(data);



            if (result.IsValid)
            {
                SituationService.TAdd(data);
                TempData["Success"] = "Situation Added Success";
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
            var Situation = SituationService.TGetById(id);
            SituationService.TDelete(Situation);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var update = SituationService.TGetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Situation data)
        {


            SituationValidator validationRules = new SituationValidator();
            ValidationResult result = validationRules.Validate(data);



            if (result.IsValid)
            {

                SituationService.TUpdate(data);
                TempData["Update"] = "Situation Update Success";
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
