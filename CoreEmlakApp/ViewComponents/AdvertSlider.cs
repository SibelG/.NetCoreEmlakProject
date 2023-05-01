using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.ViewComponents
{
    public class AdvertSlider:ViewComponent
    {
        private ImagesService _imagesService;

        public AdvertSlider(ImagesService imagesService)
        {
            _imagesService = imagesService;
        }
        public IViewComponentResult Invoke()
        {
            var list = _imagesService.List(x => x.Status == true);
            return View(list);
        }
    }
}
