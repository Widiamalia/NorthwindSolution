using Microsoft.AspNetCore.Mvc;
using Northwind.Web.Models;
using Northwind.Web.Repository;
using System;
using System.Collections.Generic;


namespace Northwind.Web.Controllers
{
    public class EmployeeController : Controller
    {

        // use dependency injection
        private readonly IEmployee _IEmployee;



        public EmployeeController(IEmployee iEmployee)
        {
            _IEmployee = iEmployee;
        }

        public IActionResult ListEmployee()
        {
            // code below is bad practice cuz break mvc pattern
/*            var listOfEmployee = new List<Employee>() {
                new Employee {Id=1001,Name="Widi Amalia Kosasih",BirthDate=new DateTime(1999,09,30)},
                new Employee {Id=1002,Name="Robbert Pattinson",BirthDate=new DateTime(1999,09,29)},
                new Employee {Id=1003,Name="Emma Watson",BirthDate=new DateTime(1999,09,28)},

            };*/

            return View("ListEmployee",_IEmployee.GetAll());

        }
        public IActionResult Category()
        {
            return View();
        }
    }
}
