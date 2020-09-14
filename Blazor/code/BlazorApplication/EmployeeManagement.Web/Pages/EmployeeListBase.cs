using EmployeeManagement.Model;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { set; get; }

        public IEnumerable<Employee> Employees { get; set; }

        protected bool ShowFooter { get; set; } = true;

        protected int SelectedEmployeesCount { get; set; } = 0;
        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }
        public async Task OnEmployeeDeleted()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
            //await Task.Run(LoadEmployees);
        }


        //private void LoadEmployees()
        //{
        //    System.Threading.Thread.Sleep(2000);

        //    Employee e1 = new Employee
        //    {
        //        EmployeeId = Guid.Parse("e5122517-92e7-42da-a445-915de6ee9717"),
        //        FirstName = "John",
        //        LastName = "Hastings",
        //        Email = "David@pragimtech.com",
        //        DateOfBrith = new DateTime(1980, 10, 5),
        //        Gender = Gender.Male,
        //        DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"),
        //        PhotoPath = "images/john.jpg"
        //    };

        //    Employee e2 = new Employee
        //    {
        //        EmployeeId = Guid.Parse("72b544d4-b703-4e43-baa9-25612d6dd7bf"),
        //        FirstName = "Sam",
        //        LastName = "Galloway",
        //        Email = "Sam@pragimtech.com",
        //        DateOfBrith = new DateTime(1981, 12, 22),
        //        Gender = Gender.Male,
        //        DepartmentId = Guid.Parse("fa87c5b3-0bbb-4ddb-a4ee-33a7a875cfe9"),
        //        PhotoPath = "images/sam.jpg"
        //    };

        //    Employee e3 = new Employee
        //    {
        //        EmployeeId = Guid.Parse("6a39fe40-8a48-492f-b5af-de38a2946b91"),
        //        FirstName = "Mary",
        //        LastName = "Smith",
        //        Email = "mary@pragimtech.com",
        //        DateOfBrith = new DateTime(1979, 11, 11),
        //        Gender = Gender.Female,
        //        DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"),
        //        PhotoPath = "images/mary.jpg"
        //    };

        //    Employee e4 = new Employee
        //    {
        //        EmployeeId = Guid.Parse("b15ace62-d94d-40fa-8e22-f048998ded80"),
        //        FirstName = "Sara",
        //        LastName = "Longway",
        //        Email = "sara@pragimtech.com",
        //        DateOfBrith = new DateTime(1982, 9, 23),
        //        Gender = Gender.Female,
        //        DepartmentId = Guid.Parse("53049207-ed06-4411-b920-9ab7de6b5c0e"),
        //        PhotoPath = "images/sara.jpg"
        //    };

        //    Employees = new List<Employee> { e1, e2, e3, e4 };
        //}
    }
}
