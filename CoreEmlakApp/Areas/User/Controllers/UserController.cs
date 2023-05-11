using CoreEmlakApp.Areas.User.Models;
using CoreEmlakApp.Areas.User.Services;
using EntityLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.DependencyResolver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Razor.Generator;

namespace CoreEmlakApp.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly RabbitMQHelper _rabbitMQHelper;
        private readonly PasswordResetRequestHandler _passwordResetRequestHandler;

        public UserController(PasswordResetRequestHandler passwordResetRequestHandler,RabbitMQHelper rabbitMQHelper,SignInManager<UserAdmin> signInManager, UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _rabbitMQHelper = rabbitMQHelper;
            _passwordResetRequestHandler = passwordResetRequestHandler;

        }

        public IActionResult Index()
        {
            return View(new LoginModel());
        }
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                UserAdmin user = await _userManager.FindByNameAsync(User.Identity.Name); 
                bool exist = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                if (exist)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user,model.NewPassword,true, true);

                        ViewBag.success = "Password changed with success";

                    }
                    else
                    {
                       
                         ModelState.AddModelError("", "An Error Occurred");
                        
                    }
                
                
                }
            }

            return View();
        }

        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordModel());
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            UserAdmin user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                string passwordResetLink = Url.Action("UpdatePassword", "User", new { userId = user.Id, token = resetToken },HttpContext.Request.Scheme);
                //MailHelper.ResetPassword.PasswordSendMail(passwordResetLink);
                _rabbitMQHelper.SendPasswordResetRequest(model.Email, passwordResetLink);

                _passwordResetRequestHandler.StartHandling();
                ViewBag.state = true;

            }
            else
            {
                ViewBag.state = false;
            }
            return View();


        }

        public IActionResult UpdatePassword(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        public async Task<IActionResult> UpdatePassword([Bind("NewPassword")] ResetPasswordModel model)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            UserAdmin user = await _userManager.FindByIdAsync(userId);  
            if(user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["Success"] = "Update with succeed";
                }
            }
            else
            {
                ModelState.AddModelError("", "There isn't such a user");
            }
            return View();

        }
        public IActionResult Profile()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            RegisterModel userViewModel = user.Adapt<RegisterModel>();
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(RegisterModel model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");

            if (ModelState.IsValid)
            {
                UserAdmin user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.FullName = model.FullName;
                user.UserName = model.UserName;
                user.Email = model.Email;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);

                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);

                    ViewBag.success = "User Information updated with success";

                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
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
            var capthchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(capthchaImage))
            {
                return Content("Recaptcha area is not empty");
            }

            var verified = await CheckCaptcha();
            if (!verified)
            {
                return Content("Not Confirmed");

            }
            if (verified)
            {
                return Content("Successfully Confirmed");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostRecaptcha()
        {
            var capthchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(capthchaImage))
            {
                return Content("Recaptcha area is not empty");
            }

            var verified = await CheckCaptcha();
            if (!verified)
            {
                return Content("Not Confirmed");

            }
            if (verified)
            {
                return Content("Successfully Confirmed");
            }
            return View();

        }
        public async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>();
            {
                new KeyValuePair<string, string>("secret", "6Le1hPolAAAAAHweuPGr - qcmgYOSiLbNvZJO - xnP");
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["google-recaptcha-response"]);
            };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));
            JObject obj = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return (bool)obj["success"];

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
                    Status = true,


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
