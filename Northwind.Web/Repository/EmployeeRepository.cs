using Northwind.Web.Models;
using System;
using System.Collections.Generic;

namespace Northwind.Web.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public List<Employee> GetAll()
        {
            var listOfEmployee = new List<Employee>() {
                new Employee {Id=1001,Name="Widi Amalia Kosasih",BirthDate=new DateTime(1999,09,30)},
                new Employee {Id=1002,Name="Robert Pattinson",BirthDate=new DateTime(1986,05,13)},
                new Employee {Id=1003,Name="Andrew Garfield",BirthDate=new DateTime(1983,08,20)},
                new Employee {Id=1004,Name="Emma Watson",BirthDate=new DateTime(1990,04,15)},


            };
            return listOfEmployee;
            //throw new System.NotImplementedException();
        }
    }
}
