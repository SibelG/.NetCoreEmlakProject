using CoreEmlakApp.Areas.Admin.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;

        public AdminController(SignInManager<UserAdmin> signInManager, UserManager<UserAdmin> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }

       
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user == null) {
                ModelState.AddModelError("", "UnSuccess");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                HttpContext.Session.SetString("Id", user.Id);
                HttpContext.Session.SetString("FullName", user.FullName);
               
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("Login");   
        }
    }
}
