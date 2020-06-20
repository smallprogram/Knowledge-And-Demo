using RESTfulApi.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.ValidationAttributes
{
    public class EmployeeNoMustDefferentFromFirstNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 获取要验证的对象
            var addDto = (EmployeeAddDto)validationContext.ObjectInstance;

            if (addDto.EmployeeNo == addDto.FirstName)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(EmployeeAddDto) });
            }


            return ValidationResult.Success;


        }
    }
}
