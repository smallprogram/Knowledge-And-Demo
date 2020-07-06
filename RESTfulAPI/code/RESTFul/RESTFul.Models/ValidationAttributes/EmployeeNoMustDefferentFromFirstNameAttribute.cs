using RESTFul.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace RESTFul.Models.ValidationAttributes
{
    /// <summary>
    /// 自定义的模型验证标记，这里是定义employeeNo不能等于employee的firstName
    /// </summary>
    public class EmployeeNoMustDefferentFromFirstNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 获取要验证的对象
            var addDto = (EmployeeAddOrUpdateDTO)validationContext.ObjectInstance;

            if (addDto.EmployeeNo == addDto.FirstName)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(EmployeeAddOrUpdateDTO) });
            }


            return ValidationResult.Success;


        }
    }
}
