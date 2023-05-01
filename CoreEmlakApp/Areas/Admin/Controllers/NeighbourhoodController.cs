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
    public class NeighbourhoodController : Controller
    {
        NeighbourhoodService neighbourhoodService;
        DistrictService districtService;

        public NeighbourhoodController(NeighbourhoodService neighbourhoodService,DistrictService districtService)
        {
            this.neighbourhoodService = neighbourhoodService;
            this.districtService = districtService;
            
        }

        public IActionResult Index()
        {
            var neighbourhood = neighbourhoodService.List(x => x.Status == true);
            return View(neighbourhood);
        }

        public IActionResult Create()
        {
            Dropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Neighbourhood data)
        {

            NeighbourhoodValidator validationRules = new NeighbourhoodValidator();
            ValidationResult result = validationRules.Validate(data);




            if (result.IsValid)
            {
                neighbourhoodService.TAdd(data);
                TempData["Success"] = "Neighbourhood Added Success";
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
            var neighbourhood = neighbourhoodService.TGetById(id);
            neighbourhoodService.TDelete(neighbourhood);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Dropdown();
            var update = neighbourhoodService.TGetById(id);
            return View(update);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Neighbourhood data)
        {


            NeighbourhoodValidator validationRules = new NeighbourhoodValidator();
            ValidationResult result = validationRules.Validate(data);



            if (result.IsValid)
            {

                neighbourhoodService.TUpdate(data);
                TempData["Update"] = "Neighbourhood Update Success";
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
            List<SelectListItem> value = (from x in districtService.List(x => x.Status == true)
                                          select new SelectListItem
                                          {
                                              Text = x.DistrictName,
                                              Value = x.DistrictId.ToString(),

                                          }).ToList();

            ViewBag.districts = value;

        }
    }
}
