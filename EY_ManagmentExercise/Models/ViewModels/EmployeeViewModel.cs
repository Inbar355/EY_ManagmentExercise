using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EY_ManagmentExercise.Models.ViewModels
{
    public class ManageAttachedEmployeesViewModel
    {
        public ManageAttachedEmployeesViewModel(int currentDepartmentId, IList<Employee> employees, string currentDepartmentName)
        {
            this.CurrentDepartmentId = currentDepartmentId;
            Employees = employees;
            CurrentDepartmentName = currentDepartmentName;
        }

        public IList<Employee> Employees { get; set; }
        public int CurrentDepartmentId { get; set; }
        public string CurrentDepartmentName { get; set; }
    }

    public class CreateNewEmployeeViewModel
    {
        public CreateNewEmployeeViewModel()
        {

        }
        public CreateNewEmployeeViewModel(int currentDepartmentId)
        {
            CurrentDepartmentId = currentDepartmentId;
        }
        public Employee Employee { get; set; }
        public int CurrentDepartmentId { get; set; }
        public string OutputMsg { get; set; }
    }
}