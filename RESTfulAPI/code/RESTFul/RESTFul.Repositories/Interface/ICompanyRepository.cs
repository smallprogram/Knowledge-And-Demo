using RESTFul.Data.Entities.AppDbEntities;
using RESTFul.Models.DtoParameters;
using RESTFul.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTFul.Repositories.Interface
{
    public interface ICompanyRepositroy
    {
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters parameters);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistsAsync(Guid companyId);


        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParameters parameters);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        Task<bool> SaveAsync();
    }

}
