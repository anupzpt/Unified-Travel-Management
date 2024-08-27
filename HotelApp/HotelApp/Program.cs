using HotelApp.Business.Accomodation;
using HotelApp.Business.Admin.Banner;
using HotelApp.Business.Admin.Blog;
using HotelApp.Business.Admin.Company;
using HotelApp.Business.Admin.Dashboard;
using HotelApp.Business.Admin.Driver;
using HotelApp.Business.Admin.FacilitySetup;
using HotelApp.Business.Admin.Gallery;
using HotelApp.Business.Admin.Guide;
using HotelApp.Business.Admin.HotelSetup;
using HotelApp.Business.Admin.Package;
using HotelApp.Business.Admin.Vechicle;
using HotelApp.Business.Home;
using HotelApp.Business.Login;
using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Business.Verification;
using HotelApp.Models;
using HotelApp.Repository.Admin.Dashboard;
using HotelApp.Repository.Admin.FacilitySetup;
using HotelApp.Repository.Dao;
using HotelApp.Repository.Generic;
using HotelApp.Repository.Home;
using HotelApp.Service;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<HotelApplicationDBContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login/Index";
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
	});
// Register the MailService with configuration settings from appsettings.json
builder.Services.AddSingleton<MailService>(provider => new MailService(
    builder.Configuration["Email:SmtpServer"],
    int.Parse(builder.Configuration["Email:SmtpPort"]),
    builder.Configuration["Email:SmtpUser"],
    builder.Configuration["Email:SmtpPass"]
));

#region Dependency Injection

// Register your services here
builder.Services.AddScoped<IHomeBusiness, HomeBusiness>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IBannerSetupBusiness, BannerSetupBusiness>();
builder.Services.AddScoped<IDashboardBusiness, DashboardBusiness>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IFacilitySetupBusiness, FacilitySetupBusiness>();
builder.Services.AddScoped<IFacilitySetupRepository, FacilitySetupRepository>();
builder.Services.AddScoped<IHotelSetupBusiness, HotelSetupBusiness>();
builder.Services.AddScoped<ICompanySetupBusiness, CompanySetupBusiness>();
builder.Services.AddScoped<IVechicleInformationBusiness, VechicleInformationBusiness>();
builder.Services.AddScoped<IPackageManagementBusiness, PackageManagementBusiness>();
builder.Services.AddScoped<IDriverSetupBusiness, DriverSetupBusiness>();
builder.Services.AddScoped<IGuideSetupBusiness, GuideSetupBusiness>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<IDapperDao, DapperDao>();
builder.Services.AddScoped<IFileHelperExtension, FileHelperExtension>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IAccomodationBusiness, AccomodationBusiness>();
builder.Services.AddScoped<IVerificationBusiness, VerificationBusiness>();
builder.Services.AddScoped<IGalleryBusiness, GalleryBusiness>();
builder.Services.AddScoped<IBlogBusiness, BlogBusiness>();

#endregion

#endregion

var app = builder.Build();

#region Middleware Configuration

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
StaticData.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
//app.UseStaticFiles(new StaticFileOptions
//{
 
//    FileProvider = new PhysicalFileProvider(@"C:\Hotel System\HotelSolution\ProjectImage"),
//    RequestPath = "/ProjectImage"
 
//});
//app.UseStaticFiles(new StaticFileOptions
//{

//    FileProvider = new PhysicalFileProvider(@"C:\Hotel System\HotelSolution\ProjectImage"),
//    RequestPath = "/ProjectImage"
//});

app.UseStaticFiles(new StaticFileOptions
{

    FileProvider = new PhysicalFileProvider(@"E:\\8thsem\\HotelManagement\\ProjectImage"),
    RequestPath = "/ProjectImage"
});
app.UseRouting();

// Ensure authentication is configured before authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application starting...");

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();

#endregion
