using EmployeeManagement.Model;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using SP.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { set; get; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public EventCallback<string> OnEmployeeDeleted { set; get; }
        [Parameter]
        public Employee Employee { get; set; }
        [Parameter]
        public bool Showfooter { get; set; }

        protected bool IsSelected { set; get; }
        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }
        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnEmployeeSelection.InvokeAsync(IsSelected);
        }

        protected ConfirmBase DeleteConfirmation { get; set; }
        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(Employee.EmployeeId.ToString());
                await OnEmployeeDeleted.InvokeAsync(Employee.EmployeeId.ToString());
            }
        }
        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }
    }
}
