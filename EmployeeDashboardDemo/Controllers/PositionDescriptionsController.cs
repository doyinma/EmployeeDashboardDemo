using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        private const int RecordsPerPage = 10;

        // GET: PositionDescriptions
        public ActionResult Index(int page = 1)
        {
            var totalRecords = db.PositionDescriptions.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);
            var positions = db.PositionDescriptions
                .Include(p => p.Division)
                .Include(p => p.Department)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(positions);
        }

        // GET: PositionDescriptions/Add
        public ActionResult Add()
        {
            using (var context = new AppDbContext())
            {
                ViewBag.Divisions = context.Divisions.ToList();
                ViewBag.Departments = context.Departments.ToList();
            }

            return View();
        }

        // POST: PositionDescriptions/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PositionDescription model, HttpPostedFileBase relatedDocument)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns if model is invalid
                using (var context = new AppDbContext())
                {
                    ViewBag.Divisions = context.Divisions.ToList();
                    ViewBag.Departments = context.Departments.ToList();
                }

                return View(model);
            }

            using (var db = new AppDbContext())
            {
                // Validate department and division exist
                var departmentExists = db.Departments.Any(d => d.Id == model.DepartmentId);
                var divisionExists = db.Divisions.Any(d => d.Id == model.DivisionId);

                if (!departmentExists)
                    ModelState.AddModelError("DepartmentId", "Invalid department selected.");
                if (!divisionExists)
                    ModelState.AddModelError("DivisionId", "Invalid division selected.");

                if (!ModelState.IsValid)
                {
                    ViewBag.Divisions = db.Divisions.ToList();
                    ViewBag.Departments = db.Departments.ToList();
                    return View(model);
                }

                // Handle file upload 
                if (relatedDocument != null && relatedDocument.ContentLength > 0)
                {
                    // Generate a unique file name
                    var fileName = Path.GetFileName(relatedDocument.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                    // Save the file to the uploads directory inside the Content folder
                    relatedDocument.SaveAs(path);

                    // Assign the saved file name to the model's property
                    model.RelatedDocument = fileName;
                }

                // Save the model to the database (as per your original logic)
                db.PositionDescriptions.Add(model);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Position added successfully.";
                return RedirectToAction("Add");
            }
        }

        // GET: PositionDescriptions/Edit/5
        public ActionResult Edit(int id)
        {
            using (var db = new AppDbContext())
            {
                var position = db.PositionDescriptions.Find(id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Divisions = db.Divisions.ToList();
                ViewBag.Departments = db.Departments.ToList();

                return View(position);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PositionDescription model, HttpPostedFileBase relatedDocument)
        {
            using (var db = new AppDbContext())
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Divisions = db.Divisions.ToList();
                    ViewBag.Departments = db.Departments.ToList();
                    return View(model);
                }

                var existing = db.PositionDescriptions.Find(model.Id);
                if (existing == null)
                {
                    return HttpNotFound();
                }

                // Update fields
                existing.Name = model.Name;
                existing.DivisionId = model.DivisionId;
                existing.DepartmentId = model.DepartmentId;
                existing.Description = model.Description;
                existing.CheckboxParkingStall = model.CheckboxParkingStall;
                existing.RadioDeskPhone = model.RadioDeskPhone;
                existing.RadioCellPhone = model.RadioCellPhone;

                // Handle file upload
                if (relatedDocument != null && relatedDocument.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(relatedDocument.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                    relatedDocument.SaveAs(path);
                    existing.RelatedDocument = fileName;
                }

                db.SaveChanges();
                TempData["SuccessMessage"] = "Position updated successfully.";
                return RedirectToAction("Edit", new { id = model.Id });
            }
        }

        // GET: PositionDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            using (var db = new AppDbContext())
            {
                var position = db.PositionDescriptions
                                 .Include("Division") 
                                 .Include("Department")
                                 .FirstOrDefault(p => p.Id == id);

                if (position == null)
                {
                    return HttpNotFound();
                }

                return View(position);
            }
        }

        // POST: PositionDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new AppDbContext())
            {
                var position = db.PositionDescriptions.Find(id);
                if (position == null)
                {
                    return HttpNotFound();
                }

                // Delete associated file if it exists
                if (!string.IsNullOrEmpty(position.RelatedDocument))
                {
                    var filePath = Server.MapPath("~/Content/uploads/" + position.RelatedDocument);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                db.PositionDescriptions.Remove(position);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Position deleted successfully.";
                return RedirectToAction("Index");
            }
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
