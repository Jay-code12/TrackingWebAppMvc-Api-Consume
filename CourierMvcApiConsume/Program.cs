using CourierMvcApiConsume.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(
   Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults
   .AuthenticationScheme
    ).AddCookie(s =>
    {
        s.LoginPath = "/Login";
        s.LogoutPath = "/Logout";
    }
 );

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options => 
options.IdleTimeout = TimeSpan.FromMinutes(5));

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

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
