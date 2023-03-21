using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "inspector")]
    public class InspectorController : Controller
    {
        ContextSystemDB context;
        Kurator? curator;
        Group? group;
        Department? department;
        List<Student> students;
        List<Event> events;
        List<Report> reports;

        List<Group> groups;
        List<Kurator> kurators;
        List<Department> departments;
        string id;
        public InspectorController(ContextSystemDB db)
        {
            context = db;
        }
        [HttpGet]
        public IActionResult Index(string idF)
        {
            if (idF != null)
            {
                group = context.Groups.Include(x=>x.Kurator).FirstOrDefault(x => x.Id == Int32.Parse(idF));
                students = context.Students.Where(x=>x.Group==group).ToList();
                events = context.Events.Where(x => x.Group == group).ToList();
                reports = context.Reports.Where(x => x.Group == group).ToList();
                ViewBag.students = students;
                ViewBag.events = events;
                ViewBag.reports = reports;
                ViewData["group"] = group.Name;
                ViewData["kurator"] = group.Kurator.Name;
            }
            departments = context.Departments.Include(x=>x.Kurators).ThenInclude(x=>x.Groups).ToList();
            ViewBag.departments = departments;
            return View();
        }
    }
}