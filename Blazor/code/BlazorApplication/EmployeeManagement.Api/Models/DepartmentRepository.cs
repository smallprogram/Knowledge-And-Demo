using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Department GetDepartment(string departmentId)
        {
            return appDbContext.Departments
                .FirstOrDefault(d => d.DepartmentId == Guid.Parse(departmentId));
        }

        public IEnumerable<Department> GetDepartments()
        {
            return appDbContext.Departments;
        }
    }
}
