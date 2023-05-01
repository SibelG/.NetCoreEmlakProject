using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.ViewComponents
{
    public class AdvertAll : ViewComponent
    {
        ImagesService _imagesService;
        AdvertService _advertService;

        public AdvertAll(ImagesService imagesService, AdvertService advertService) {
            _imagesService = imagesService;
            _advertService = advertService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _advertService.List(x => x.Status == true);
            var images = _imagesService.List(x=>x.Status == true);
            ViewBag.img = images;
            return View(list);


        }
    }
}
