using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreEmlakApp.Areas.Admin.Identity;
using DataAccessLayer.Abstract;
using DataAccessLayer.Data;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddDbContext<DataContext>(conf => conf.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());


builder.Services.AddIdentity<UserAdmin, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedAccount = false; 

    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.AllowedUserNameCharacters = "abcçdefgðhiýjklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-";
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Admin/Admin/Login/";
    opt.LogoutPath = "/Admin/Admin/LogOut/";
    opt.AccessDeniedPath = "/Admin/Admin/AccessDeniedPath/";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(6);
});

builder.Services.AddSession();

builder.Services.AddScoped<IAdvertRepository, EfAdvertRepository>();
builder.Services.AddScoped<AdvertService, AdvertManager>();

builder.Services.AddScoped<ICityRepository, EfCityRepository>();
builder.Services.AddScoped<CityService, CityManager>();

builder.Services.AddScoped<IDistrictRepository, EfDistrictRepository>();
builder.Services.AddScoped<DistrictService, DistrictManager>();

builder.Services.AddScoped<INeighbourhoodRepository, EfNeighbourhoodRepository>();
builder.Services.AddScoped<NeighbourhoodService, NeigbourhoodManager>();

builder.Services.AddScoped<IGeneralSettingsRepository, EfGeneralSettingsRepository>();
builder.Services.AddScoped<GeneralSettingsService, GeneralSettingsManager>();

builder.Services.AddScoped<IImagesRepository, EfImagesRepository>();
builder.Services.AddScoped<ImagesService, ImagesManager>();

builder.Services.AddScoped<ISituationRepository, EfSituationRepository>();
builder.Services.AddScoped<SituationService, SituationManager>();

builder.Services.AddScoped<IFuelTypeRepository, EfFuelTypeRepository>();
builder.Services.AddScoped<FuelTypeService, FuelTypeManager>();

builder.Services.AddScoped<IFrontRepository, EfFrontRepository>();
builder.Services.AddScoped<FrontService, FrontManager>();

builder.Services.AddScoped<ITypeRepository, EfTypeRepository>();
builder.Services.AddScoped<TypeService, TypeManager>();

builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<CategoryService, CategoryManager>();

builder.Services.AddScoped<IProjectImageRepository, EfProjectImageRepository>();
builder.Services.AddScoped<ProjectImageService, ProjectImageManager>();

builder.Services.AddScoped<IHeadingRepository, EfHeadingRepository>();
builder.Services.AddScoped<HeadingService, HeadingManager>();

builder.Services.AddScoped<IProjectRepository, EfProjectRepository>();
builder.Services.AddScoped<ProjectService, ProjectManager>();


var app = builder.Build();


app.PrepareDatabase().GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();   
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=User}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
