using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EY_ManagmentExercise.Models.ViewModels
{
    public class CreateNewDepartmentViewModel
    {
        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }
        public string OutputMsg { get; set; }
    }
}