using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.ViewComponents
{
    public class Footer : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
