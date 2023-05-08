using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.ViewComponents
{
    public class Head : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
