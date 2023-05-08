using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.ViewComponents
{
    public class Projects:ViewComponent
    {
        ProjectService _projectService;
        ProjectImageService _projectImageService;

        public Projects(ProjectService projectService ,ProjectImageService projectImageService) 
        { 
        
            _projectService = projectService;
            _projectImageService = projectImageService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _projectService.List(x => x.Status == true);
            var images = _projectImageService.List(x => x.Status == true);
            ViewBag.images = images;
            return View(list);
            


        }
    }
}
