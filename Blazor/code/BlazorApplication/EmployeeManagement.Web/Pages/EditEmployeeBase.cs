using EmployeeManagement.Model;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { set; get; }

        private Employee Employee { set; get; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { set; get; } = new EditEmployeeModel();

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        //public string DepartmentId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(Id);
            Departments = (await DepartmentService.GetDepartments()).ToList();
            //DepartmentId = Employee.DepartmentId.ToString();
            EditEmployeeModel.ConfirmEmail = Employee.Email;
            EditEmployeeModel.DateOfBrith = Employee.DateOfBrith;
            EditEmployeeModel.Department = Employee.Department;
            EditEmployeeModel.DepartmentId = Employee.DepartmentId;
            EditEmployeeModel.Email = Employee.Email;
            EditEmployeeModel.EmployeeId = Employee.EmployeeId;
            EditEmployeeModel.Gender = Employee.Gender;
            EditEmployeeModel.LastName = Employee.LastName;
            EditEmployeeModel.FirstName = Employee.FirstName;
            EditEmployeeModel.PhotoPath = Employee.PhotoPath;
        }

        protected void HandleValidSubmit()
        {

        }
    }
}
