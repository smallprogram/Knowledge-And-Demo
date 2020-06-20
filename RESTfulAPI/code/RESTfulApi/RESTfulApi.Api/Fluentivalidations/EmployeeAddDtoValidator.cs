using FluentValidation;
using RESTfulApi.Api.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Fluentivalidations
{
    public class EmployeeAddDtoValidator : AbstractValidator<EmployeeAddDto>
    {
        public EmployeeAddDtoValidator()
        {
            RuleFor(employee => employee.EmployeeNo).NotNull().NotEmpty();
        }
    }
}
