using NoName.Data.Context;
using Microsoft.EntityFrameworkCore;
using NoName.Data.Repositories;
using NoName.Business.Services;
using NoName.Business.Manager;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<NoNameContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));

builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddDataProtection();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = new PathString("/");
	options.LogoutPath = new PathString("/");
	options.AccessDeniedPath = new PathString("/");

});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"

    );


app.MapDefaultControllerRoute();


app.Run();
