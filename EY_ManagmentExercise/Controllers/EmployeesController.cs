using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EY_ManagmentExercise.Models;
using EY_ManagmentExercise.Models.ViewModels;

namespace EY_ManagmentExercise.Controllers
{
    public class EmployeesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index(int id, string name)
        {
            var employee = db.Employees.Where(e => e.Department_id == id).Select(e => e);
            ManageAttachedEmployeesViewModel viewModel = new ManageAttachedEmployeesViewModel(id, employee.ToList(), name);
            return View(viewModel);
        }

        public ActionResult Create(int? CurrentDepartmentId)
        {
            if (CurrentDepartmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateNewEmployeeViewModel createEmployeeViewModel = new CreateNewEmployeeViewModel((int)CurrentDepartmentId);
            return View(createEmployeeViewModel);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewEmployeeViewModel createEmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                if (db.Employees.Find(createEmployeeViewModel.Employee.ID) != null)
                {
                    createEmployeeViewModel.OutputMsg = $"Pay Attention! - Invalid create attempt - Employee with ID of '{createEmployeeViewModel.Employee.ID}' already exists.";
                    return View(createEmployeeViewModel);
                }
                createEmployeeViewModel.Employee.Department_id = createEmployeeViewModel.CurrentDepartmentId;
                db.Employees.Add(createEmployeeViewModel.Employee);
                db.SaveChanges();
                TempData["AlertMessage"] = "The Employee was successfully created";
                return RedirectToAction("Index", new { id = createEmployeeViewModel.CurrentDepartmentId});
            }

            return View(createEmployeeViewModel);
        }

        public ActionResult Remove(int? employeeId, int departmentId)
        {
            if (employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(employeeId);
            employee.Department_id = null;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            TempData["AlertMessage"] = "The Employee was successfully removed from the Department";
            return RedirectToAction("Index", new {id = departmentId });
        }

        public ActionResult Details(int CurrentDepartmentId, string CurrentDepartmentName)
        {
            var employee = db.Employees.Where(e => (e.Department_id == null || e.Department_id != CurrentDepartmentId)).Select(e => e);
            ManageAttachedEmployeesViewModel viewModel = new ManageAttachedEmployeesViewModel(CurrentDepartmentId, employee.ToList(), CurrentDepartmentName);
            return View(viewModel);
        }

        public ActionResult AddEmployee(int? currentDepartmentId, int? employeeId)
        {
            if (employeeId == null || currentDepartmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(employeeId);
            employee.Department_id = currentDepartmentId;
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            TempData["AlertMessage"] = "The Employee was successfully added to this Department";
            return RedirectToAction("Index", new { id = currentDepartmentId });
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
