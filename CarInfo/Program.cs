using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarInfo.Data;
using CarInfo.Areas.Identity.Data;
using CarInfo.Interfaces;
using CarInfo.Models.CatalogModel;
using CarInfo.Mapping;
using CarInfo.Models.CarTypeModel;
using CarInfo.Models.SuperCarModel;
using CarInfo.Models.CarModel;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CarInfoContextConnection") ?? throw new InvalidOperationException("Connection string 'CarInfoContextConnection' not found.");

builder.Services.AddDbContext<CarInfoContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CarInfoContext>().AddDefaultTokenProviders().AddDefaultUI();

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IMapper<CarInfo.Entities.Catalog, CatalogModel>, CatalogMapping>();

builder.Services.AddScoped<ICarTypeService, CarTypeService>();
builder.Services.AddScoped<IMapper<CarInfo.Entities.CarType, CarTypeModel>, CarTypeMapping>();

builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<IMapper<CarInfo.Entities.CarModel, CarsModelModel>, CarModelMapping>();

builder.Services.AddScoped<ISuperCarService, SuperCarService>();
builder.Services.AddScoped<IMapper<CarInfo.Entities.SuperCar, SuperCarModel>, SuperCarMapping>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
