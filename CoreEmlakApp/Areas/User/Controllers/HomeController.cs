using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult Footer()
        {
            return PartialView();
        }
        public PartialViewResult Head()
        {
            return PartialView();
        }
        public PartialViewResult Header()
        {
            return PartialView();
        }
    }
}
