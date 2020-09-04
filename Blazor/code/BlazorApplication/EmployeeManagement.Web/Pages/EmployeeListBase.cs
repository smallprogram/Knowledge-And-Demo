using EmployeeManagement.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }


        protected override Task OnInitializedAsync()
        {
            LoadEmployees();
            return base.OnInitializedAsync();
        }

        private void LoadEmployees()
        {
            Employee e1 = new Employee
            {
                EmployeeId = Guid.Parse("e5122517-92e7-42da-a445-915de6ee9717"),
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBrith = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                Department = new Department { DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"), DepartmentName = "IT" },
                PhotoPath = "images/john.jpg"
            };

            Employee e2 = new Employee
            {
                EmployeeId = Guid.Parse("72b544d4-b703-4e43-baa9-25612d6dd7bf"),
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBrith = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                Department = new Department { DepartmentId = Guid.Parse("fa87c5b3-0bbb-4ddb-a4ee-33a7a875cfe9"), DepartmentName = "HR" },
                PhotoPath = "images/sam.jpg"
            };

            Employee e3 = new Employee
            {
                EmployeeId = Guid.Parse("6a39fe40-8a48-492f-b5af-de38a2946b91"),
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBrith = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                Department = new Department { DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"), DepartmentName = "IT" },
                PhotoPath = "images/mary.jpg"
            };

            Employee e4 = new Employee
            {
                EmployeeId = Guid.Parse("b15ace62-d94d-40fa-8e22-f048998ded80"),
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBrith = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                Department = new Department { DepartmentId = Guid.Parse("53049207-ed06-4411-b920-9ab7de6b5c0e"), DepartmentName = "Payroll" },
                PhotoPath = "images/sara.jpg"
            };

            Employees = new List<Employee> { e1, e2, e3, e4 };
        }
    }
}
