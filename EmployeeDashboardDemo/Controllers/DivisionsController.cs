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
    public class DivisionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Divisions
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var divisions = db.Divisions.OrderByDescending(d => d.Id).ToPagedList(pageNumber, pageSize);
            return View(divisions);
        }

        // GET: Divisions/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Divisions/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Divisions.Add(division);
                db.SaveChanges();
                TempData["Success"] = "Division added successfully.";
                return RedirectToAction("Add");
            }

            return View(division);
        }

        // GET: Divisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Division division = db.Divisions.Find(id);
            if (division == null)
                return HttpNotFound();

            return View(division);
        }

        // POST: Divisions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Division division)
        {
            if (ModelState.IsValid)
            {
                db.Entry(division).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Division updated successfully.";
                return RedirectToAction("Index");
            }

            return View(division);
        }

        // GET: Divisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Division division = db.Divisions.Find(id);
            if (division == null)
                return HttpNotFound();

            return View(division);
        }

        // POST: Divisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = db.Divisions.Find(id);
            if (division != null)
            {
                db.Divisions.Remove(division);
                db.SaveChanges();
                TempData["Success"] = "Division deleted successfully.";
            }

            return RedirectToAction("Index");
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
