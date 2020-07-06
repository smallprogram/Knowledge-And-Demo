using RESTFul.Data.Entities.AppDbEntities;
using RESTFul.Models.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RESTFul.Models.Dto
{
    [EmployeeNoMustDefferentFromFirstName(ErrorMessage = "员工编号必须和名不一样")]
    public abstract class EmployeeAddOrUpdateDTO : IValidatableObject
    {
        //public Guid Id { get; set; }
        //public Guid CompanyId { get; set; }
        [DisplayName("员工号")]
        [Required(ErrorMessage = "{0}是必填的")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}的长度为{1}")]
        public string EmployeeNo { get; set; }
        [Display(Name = "名")]
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string FirstName { get; set; }
        [Display(Name = "姓"), Required(ErrorMessage = "{0}是必填的"), MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string LastName { get; set; }
        [Display(Name = "性别")]
        public Gender Gender { get; set; }
        [Display(Name = "出生日期")]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能一样", new[] { nameof(EmployeeAddOrUpdateDTO) });
                //yield return new ValidationResult("姓和名不能一样",new[] { nameof(FirstName),nameof(LastName) });
            }

            if (!Enum.IsDefined(typeof(Gender), Gender))
            {
                yield return new ValidationResult("性别值错误，只能为1或2，1代表男，2代表女", new[] { nameof(EmployeeAddOrUpdateDTO) });
            }

            if (DateOfBirth > DateTime.Parse("2020-01-01") || DateOfBirth < DateTime.Parse("1980-01-01"))
            {
                yield return new ValidationResult("年龄范围必须在1980年到2020年之间", new[] { nameof(EmployeeAddOrUpdateDTO) });
            }
        }
    }
}
