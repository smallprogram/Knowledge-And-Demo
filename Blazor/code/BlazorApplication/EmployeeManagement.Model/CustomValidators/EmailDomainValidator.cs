using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Model.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowedDomain { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString().Contains("@"))
            {
                string[] strings = value.ToString().Split('@');
                if (strings[1].ToUpper() == AllowedDomain.ToUpper())
                {
                    return null;
                }
                return new ValidationResult($"邮箱域名必须为{AllowedDomain}", new[] { validationContext.MemberName });
            }
            return new ValidationResult($"邮箱域名必须为{AllowedDomain}", new[] { validationContext.MemberName });
        }
    }
}
