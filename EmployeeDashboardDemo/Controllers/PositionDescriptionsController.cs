using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeDashboardDemo.Models;

namespace EmployeeDashboardDemo.Controllers
{
    public class PositionDescriptionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PositionDescriptions
        public ActionResult Index()
        {
            var positionDescriptions = db.PositionDescriptions.Include(p => p.Department).Include(p => p.Division);
            return View(positionDescriptions.ToList());
        }

        // GET: PositionDescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionDescription positionDescription = db.PositionDescriptions.Find(id);
            if (positionDescription == null)
            {
                return HttpNotFound();
            }
            return View(positionDescription);
        }

        // GET: PositionDescriptions/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name");
            return View();
        }

        // POST: PositionDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DivisionId,DepartmentId,Description,CheckboxParkingStall,RadioDeskPhone,RadioCellPhone,RelatedDocument,DateCreated")] PositionDescription positionDescription)
        {
            if (ModelState.IsValid)
            {
                db.PositionDescriptions.Add(positionDescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", positionDescription.DepartmentId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", positionDescription.DivisionId);
            return View(positionDescription);
        }

        // GET: PositionDescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionDescription positionDescription = db.PositionDescriptions.Find(id);
            if (positionDescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", positionDescription.DepartmentId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", positionDescription.DivisionId);
            return View(positionDescription);
        }

        // POST: PositionDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DivisionId,DepartmentId,Description,CheckboxParkingStall,RadioDeskPhone,RadioCellPhone,RelatedDocument,DateCreated")] PositionDescription positionDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(positionDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", positionDescription.DepartmentId);
            ViewBag.DivisionId = new SelectList(db.Divisions, "Id", "Name", positionDescription.DivisionId);
            return View(positionDescription);
        }

        // GET: PositionDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionDescription positionDescription = db.PositionDescriptions.Find(id);
            if (positionDescription == null)
            {
                return HttpNotFound();
            }
            return View(positionDescription);
        }

        // POST: PositionDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PositionDescription positionDescription = db.PositionDescriptions.Find(id);
            db.PositionDescriptions.Remove(positionDescription);
            db.SaveChanges();
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
