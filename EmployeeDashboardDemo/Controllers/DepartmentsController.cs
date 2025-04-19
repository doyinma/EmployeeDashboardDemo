using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeDashboardDemo.Models;
using PagedList;

namespace EmployeeDashboardDemo.Controllers
{
    public class DepartmentsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Departments
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var departments = db.Departments
                                .Include(d => d.Division)
                                .OrderBy(d => d.Id);

            return View(departments.ToPagedList(page, pageSize));
        }

        // GET: Departments/Add
        public ActionResult Add()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions.ToList(), "Id", "Name");
            return View();
        }

        // POST: Departments/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
            {
                ModelState.AddModelError("Name", "Please enter a department name.");
            }

            if (department.DivisionId == 0)
            {
                ModelState.AddModelError("DivisionId", "Please select a division.");
            }

            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                TempData["Success"] = "Department added successfully.";
                return RedirectToAction("Add");
            }

            ViewBag.DivisionId = new SelectList(db.Divisions.ToList(), "Id", "Name", department.DivisionId);
            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var department = db.Departments.Find(id);
            if (department == null)
                return HttpNotFound();

            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", department.DivisionId);
            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DivisionId")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { success = 1 });
            }

            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", department.DivisionId);
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Eager-load the Division navigation property
            var department = db.Departments
                               .Include(d => d.Division)
                               .FirstOrDefault(d => d.Id == id);

            if (department == null)
                return HttpNotFound();

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var department = db.Departments.Find(id);
            if (department != null)
            {
                db.Departments.Remove(department);
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { success = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
