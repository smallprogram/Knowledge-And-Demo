using AutoMapper;
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
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Inject]
        public IMapper Mapper { set; get; }
        [Inject]
        public NavigationManager NavigationManager { set; get; }
        [Parameter]
        public string Id { get; set; }

        private Employee Employee { set; get; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { set; get; } = new EditEmployeeModel();
        public List<Department> Departments { get; set; } = new List<Department>();
        public string PageHeader { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id == null || Id == "")
            {
                PageHeader = "CreateEmployee";
                Employee = new Employee
                {
                    DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"),
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/nomal_head.jpg",
                    //Department = await DepartmentService.GetDepartment("cbcebfdc-d176-4045-a680-75d5893fe185")
                };
            }
            else
            {
                PageHeader = "EditEmployee";
                Employee = await EmployeeService.GetEmployee(Id);
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();

            Mapper.Map(Employee, EditEmployeeModel);
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);
            Employee result = null;
            if (Employee.EmployeeId == Guid.Empty)
            {
                result = await EmployeeService.CreateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(EditEmployeeModel.EmployeeId.ToString());
            NavigationManager.NavigateTo("/");
        }
    }
}
