
using CoreEmlakApp.Areas.Admin.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CoreEmlakApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUserManagementController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private SignInManager<UserAdmin> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminUserManagementController(SignInManager<UserAdmin> signInManager, UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var list = _userManager.Users.Where(x=>x.Status==true).ToList(); 
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterModel model)
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

                var result = await _userManager.CreateAsync(user, model.Password);

                var resultRole = await _userManager.AddToRoleAsync(user, role.Name);

                if (result.Succeeded || resultRole.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public async Task<IActionResult> UserDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Status = false;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("DeleteList");
        }
        public IActionResult DeleteList()
        {
            var list = _userManager.Users.Where(x => x.Status == false).ToList();
            return View(list);
        }
        public async Task<IActionResult> UsersActive(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.Status = true;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        

        public async Task<IActionResult> RoleAssign(string id)
        {
            UserAdmin user = await _userManager.FindByIdAsync(id);
            TempData["userId"] = id;

            ViewBag.FullName = user.FullName;
            IQueryable<IdentityRole> roles = _roleManager.Roles;

            List<string> userRoles = await  _userManager.GetRolesAsync(user) as List<string>;
            List<RoleAssignViewModel> roleAssignViewModel = new List<RoleAssignViewModel>(); 

            foreach(var role in roles)
            {
                RoleAssignViewModel r = new RoleAssignViewModel();

                if (userRoles.Contains(role.Name))
                {
                    r.Id = role.Id;
                    r.Name = role.Name;
                    r.Exist = true;
                }
                else
                {
                    r.Id = role.Id;
                    r.Name = role.Name;
                    r.Exist = false;
                }
                roleAssignViewModel.Add(r);
            }

            return View(roleAssignViewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAssign (List<RoleAssignViewModel> roleAssignViewModels)
        {
            UserAdmin user = await _userManager.FindByIdAsync(TempData["userId"].ToString());
            
            foreach(var item in  roleAssignViewModels)
            {
                if (item.Exist)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);

                }
            }
            return RedirectToAction("Index");   

        }
    }
}
