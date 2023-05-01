using CoreEmlakApp.Areas.User.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmlakApp.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserController(SignInManager<UserAdmin> signInManager, UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(new LoginModel());
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "There is no such user");
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                ModelState.AddModelError("", "User locked for a while");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                HttpContext.Session.SetString("Id", user.Id);
                HttpContext.Session.SetString("FullName", user.FullName);

                return RedirectToAction("Index");
            }
            else
            {
                await _userManager.AccessFailedAsync(user);
                int fail = await _userManager.GetAccessFailedCountAsync(user);

                if (fail == 3)
                {
                    await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(2)));

                }
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                UserAdmin user = new UserAdmin()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,


                };

                IdentityRole role = new IdentityRole()
                {
                    Name = "User"
                };
                await _roleManager.CreateAsync(role);

                var result = await _userManager.CreateAsync(user,model.Password);

                var resultRole = await  _userManager.AddToRoleAsync(user, role.Name);
                
                if(result.Succeeded || resultRole.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                return View();
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("FullName");
            return RedirectToAction("Login");
        }
    }

        
}
