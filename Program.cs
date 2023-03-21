using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextSystemDB>(options => options.UseSqlite(connection));
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index";
        options.AccessDeniedPath = "/Home/Accessdenied";
    });
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapPost("/Login", async (string? returnUrl, HttpContext context, ContextSystemDB db) =>
{
    var form = context.Request.Form;
    if (!form.ContainsKey("login") || !form.ContainsKey("password"))
        return Results.BadRequest("дані не введені");
    string login = form["login"];
    string password = form["password"];
    Kurator? kurator = db.Kurators.FirstOrDefault(x => x.Login == login && x.Password == password);
    if (kurator != null)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, kurator.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, "kurator")
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await context.SignInAsync(claimsPrincipal);
        returnUrl = "/Curator/Index";
        return Results.Redirect(returnUrl ?? "/");
    }
    else if (form["login"] == "Inspector" && form["password"] == "pass")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, "-1"),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, "inspector")
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await context.SignInAsync(claimsPrincipal);
        returnUrl = "/Inspector/Index";
        return Results.Redirect(returnUrl ?? "/");
    }
    else
    {
        return Results.Unauthorized();
    }

});

app.MapGet("/Logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return "Дані видалені";
});

app.MapControllerRoute(name: "default", pattern: "{controller=Curator}/{action=Index}/{id?}");

app.Run();
