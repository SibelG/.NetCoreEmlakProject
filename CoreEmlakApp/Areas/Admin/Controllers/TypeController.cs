using BusinessLayer.Abstract;
using BusinessLayer.ValidadionRules;
using EntityLayer.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypeController : Controller
    {
        TypeService typeService;
        SituationService situationService;


        public TypeController(TypeService typeService, SituationService situationService)
        {
            this.typeService = typeService;
            this.situationService = situationService;
        }

        public IActionResult Index()
        {
            var type = typeService.List(x => x.Status == true);
            return View(type);
        }

        public IActionResult Create()
        {
            Dropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EntityLayer.Entities.Type data)
        {

            TypeValidator validationRules = new TypeValidator();
            ValidationResult result = validationRules.Validate(data);




            if (result.IsValid)
            {
                typeService.TAdd(data);
                TempData["Success"] = "Type Added Success";
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

        public IActionResult Delete(int id)
        {
            var district = typeService.TGetById(id);
            typeService.TDelete(district);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Dropdown();
            var update = typeService.TGetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EntityLayer.Entities.Type data)
        {


            TypeValidator validationRules = new TypeValidator();
            ValidationResult result = validationRules.Validate(data);



            if (result.IsValid)
            {

                typeService.TUpdate(data);
                TempData["Update"] = "Type Update Success";
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
            List<SelectListItem> value = (from x in situationService.List(x => x.Status == true)
                                          select new SelectListItem
                                          {
                                              Text = x.SituationName,
                                              Value = x.SituationId.ToString(),

                                          }).ToList();

            ViewBag.situations = value;

        }
    }
}
