using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task<Employee> CreateEmployee(Employee newEmployee);
    }
}
