using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(string departmentId);
    }
}
