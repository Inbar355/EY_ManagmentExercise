using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EY_ManagmentExercise.Models;
using EY_ManagmentExercise.Models.ViewModels;

namespace EY_ManagmentExercise.Controllers
{
    public class DepartmentsController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewDepartmentViewModel departmenViewtModel)
        {
            if (ModelState.IsValid)
            {
                if (db.Departments.Find(departmenViewtModel.Department.Department_id) != null)
                {
                    departmenViewtModel.OutputMsg = $"Invalid create attempt - department with ID of '{departmenViewtModel.Department.Department_id}' already exists.";
                    return View(departmenViewtModel);
                }
                departmenViewtModel.Department.LastUpdateDate = DateTime.Now;
                departmenViewtModel.Department.CreationDate = DateTime.Now;
                db.Departments.Add(departmenViewtModel.Department);
                db.SaveChanges();
                TempData["AlertMessage"] = $"Department with Id '{departmenViewtModel.Department.Department_id}' was succesfully created!";
                return RedirectToAction("Index");
            }

            return View(departmenViewtModel);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Department_id,CreationDate,LastUpdateDate,Name,Description,IsActive,CreatedBy")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["AlertMessage"] = $"Department with Id '{department.Department_id}' was succesfully edited!";
                return RedirectToAction("Index");
            }
            return View(department);
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
