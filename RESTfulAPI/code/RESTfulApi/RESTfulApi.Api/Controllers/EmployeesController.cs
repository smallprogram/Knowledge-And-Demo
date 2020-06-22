using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfulApi.Api.Entities;
using RESTfulApi.Api.Models;
using RESTfulApi.Api.Services;

namespace RESTfulApi.Api.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICompanyRepositroy _companyRepositroy;
        private readonly IMapper _mapper;

        public EmployeesController(ICompanyRepositroy companyRepositroy, IMapper mapper)
        {
            _companyRepositroy = companyRepositroy ?? throw new ArgumentNullException(nameof(companyRepositroy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId,
            [FromQuery(Name = "gender")] string genderDisplay,
            string q)
        {

            if (!await _companyRepositroy.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepositroy.GetEmployeesAsync(companyId, genderDisplay, q);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId}", Name = nameof(GetEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {

            if (!await _companyRepositroy.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employee = await _companyRepositroy.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employee)
        {
            if (!await _companyRepositroy.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Employee>(employee);
            _companyRepositroy.AddEmployee(companyId, entity);
            await _companyRepositroy.SaveAsync();

            var dtoToReturn = _mapper.Map<EmployeeDto>(entity);

            return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = dtoToReturn.Id }, dtoToReturn);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeForCompany(Guid companyId, Guid employeeId,EmployeeUpdateDto employeeUpdateDto)
        {
            if (!await _companyRepositroy.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepositroy.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                //return NotFound();

                var employeeToAddEntity = _mapper.Map<Employee>(employeeUpdateDto);
                employeeToAddEntity.Id = employeeId;

                _companyRepositroy.AddEmployee(companyId, employeeToAddEntity);
                await _companyRepositroy.SaveAsync();

                var dtoToReturn = _mapper.Map<EmployeeDto>(employeeToAddEntity);

                return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = dtoToReturn.Id }, dtoToReturn);
            }

            // 更新针对的是资源而不是数据库实体
            // 也就是针对DTO的更新
            // 这里是需要先把entity转换为updateDto
            // 再使用传入的DTO更新从entity转换的DTO
            // 再将更新后的DTO转换entity，然后更新回数据库

            _mapper.Map(employeeUpdateDto, employeeEntity);


            _companyRepositroy.UpdateEmployee(employeeEntity);

            await _companyRepositroy.SaveAsync();

            return NoContent();
            // return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = employeeId }, employeeUpdateDto);
        }
    }
}
