using EmployeeDashboardDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EmployeeDashboardDemo.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var divisions = db.Divisions.OrderByDescending(d => d.Id).Take(5).ToList();
            var departments = db.Departments
                                .Include(d => d.Division)
                                .OrderByDescending(d => d.Id)
                                .Take(5).ToList();
            var positions = db.PositionDescriptions
                              .Include(p => p.Division)
                              .Include(p => p.Department)
                              .OrderByDescending(p => p.Id)
                              .Take(5).ToList();

            var model = new DashboardViewModel
            {
                Divisions = divisions,
                Departments = departments,
                Positions = positions
            };

            return View(model);
        }
    }
}