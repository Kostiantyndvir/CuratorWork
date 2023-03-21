using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    ContextSystemDB context;
    public HomeController(ContextSystemDB db) { context = db; }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Registration()
    {
        List<Department> departments= context.Departments.ToList();
        return View(departments);
    }
    [HttpPost]
    public IActionResult Registration(string name, string login, string password, string department, string phone, string email)
    {
        Department? depart = context.Departments.FirstOrDefault(x => x.Name == department);
        Kurator kurator = new() { 
            Name = name, Login = login, 
            Password = password, Department = depart, 
            Phone = phone, Email = email };
        context.Kurators.Add(kurator);
        context.SaveChanges();
        return View("Index");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
