using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Data;
using System.Xml;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "kurator")]
    public class CuratorController : Controller
    {
        ContextSystemDB context;
        Kurator? curator;
        Group? group;
        Group? groupFromForm;
        List<Group> groups;
        List<Student> studs;
        List<Event> events;
        List<Report> reports;
        string id;
        public CuratorController(ContextSystemDB db) 
        { 
            context = db;
        }
        void settings()
        {
            id = HttpContext.User.Identity.Name;
            curator = context.Kurators.Find(Int32.Parse(id));
            group = context.Groups.FirstOrDefault(x => x.Kurator == curator);
            groups = context.Groups.Where(x => x.Kurator == curator).ToList();
            if (groupFromForm != null)
            {
                group = groupFromForm;
            }
            studs = context.Students.Where(x => x.Group == group).ToList();
            reports = context.Reports.Where(x => x.Group == group).ToList();
            events = context.Events.Where(x => x.Group == group).ToList();
            ViewData["group"] = group?.Name;
            ViewBag.Reports = reports;
            ViewBag.Events = events;
            ViewBag.Studs = studs;
            ViewBag.Groups = groups;
        }
        public IActionResult Index(string name)
        {
            settings();
            if (name != null)
            {
                Group gr = new() { Name = name, Kurator= curator };
                context.Groups.Add(gr);
                context.SaveChanges();
            }
            groups = context.Groups.Where(x => x.Kurator == curator).ToList();
            ViewBag.Groups = groups;
            return View();
        }
        public IActionResult GhangeGroup(string groupC)
        {
            groupFromForm = context.Groups.FirstOrDefault(x=>x.Name==groupC);
            settings();
            return View("Students");
        }
        [HttpGet]
        public IActionResult Students()
        {
            settings();
            return View();
        }
        [HttpPost]
        public IActionResult Students(
            string group, string addressstudy, string addresshome, string activities,
            string parentphone, string phone, string parents, string name) {
            settings();

            if (group!=null && addressstudy != null && addresshome != null && activities != null 
                && parentphone != null && phone != null && parents != null && name != null)
            {
                Group? gr = context.Groups.FirstOrDefault(x => x.Name == group);
                Student st = new();
                st.Activities = activities;
                st.ParentPhone = parentphone;
                st.Phone = phone;
                st.Parents = parents;
                st.AddressHome = addresshome;
                st.Name = name;
                st.AddressStudy = addressstudy;
                st.Group = gr;
                context.Students.Add(st);
                context.SaveChanges();
                List<Group> groups = context.Groups.Where(x => x.Kurator == curator).ToList();
                List<Student> studs = context.Students.Where(x => x.Group == gr).ToList();
                ViewBag.Studs = studs;
                ViewBag.Groups = groups;
            }
            return View();
        }
        [HttpGet]
        public IActionResult Reports()
        {
            settings();
            return View();
        }
        [HttpPost]
        public IActionResult Reports(string description, string interval, string group)
        {
            if (group != null && description != null && interval != null)
            {
                groupFromForm = context.Groups.FirstOrDefault(x => x.Name == group);
                Report report = new();
                report.Interval = interval;
                report.Description = description;
                report.Group = groupFromForm;
                report.Date = DateOnly.FromDateTime(DateTime.Now);
                context.Reports.Add(report);
                context.SaveChanges();
            }
            settings();
            return View();
        }
        [HttpGet]
        public IActionResult Events()
        {
            settings();
            return View();
        }
        [HttpPost]
        public IActionResult Events(string name, string group, DateOnly date)
        {
            if (group != null  && name != null)
            {
                groupFromForm = context.Groups.FirstOrDefault(x => x.Name == group);
                Event ev = new();
                ev.Name = name;
                ev.Group = groupFromForm;
                ev.Date = date;
                context.Events.Add(ev);
                context.SaveChanges();
            }
            settings();
            return View();
        }
    }
}
