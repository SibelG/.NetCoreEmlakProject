using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.ViewComponents
{
    public class AdvertSlider:ViewComponent
    {
        private ProjectImageService _projectImageService;

        public AdvertSlider(ProjectImageService projectImageService)
        {
            _projectImageService = projectImageService;
        }
        public IViewComponentResult Invoke()
        {
            var list = _projectImageService.List(x => x.Status == true);
            return View(list);
        }
    }
}
