using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await httpClient.GetJsonAsync<Employee>($"api/employees/{id}");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await httpClient.GetJsonAsync<Employee[]>("api/employees");
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            return await httpClient.PutJsonAsync<Employee>("api/employees",updatedEmployee);

        }
    }
}
