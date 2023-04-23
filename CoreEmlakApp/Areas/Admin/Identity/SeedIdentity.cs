using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreEmlakApp.Areas.Admin.Identity
{
    public static class SeedIdentity
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedService.ServiceProvider;
            await Seed(serviceProvider);

            return app;
        }
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<UserAdmin>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var user = new UserAdmin()
            {
                FullName = "Admin Admin",
                UserName = "admin",
                Email = "sbl.gencturks@gmail.com"
            };
            if (await userManager.FindByNameAsync("Admin Admin") == null)
            {
                var IdentityResult = await userManager.CreateAsync(user,"admin123456");
            }
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Admin"
                };

                var IdentityResult = await roleManager.CreateAsync(role);
                var Result = await userManager.AddToRoleAsync(user, role.Name);

            }
        }
    }
   
}
