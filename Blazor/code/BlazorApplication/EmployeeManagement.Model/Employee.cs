﻿using EmployeeManagement.Model.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage ="姓必须要填写")]
        [MinLength(2,ErrorMessage ="姓的长度不能小于2")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "名必须要填写")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Email格式不对！")]
        [EmailDomainValidator(AllowedDomain ="gmail.com")]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public Guid DepartmentId { get; set; }
        public string PhotoPath { get; set; }
        public Department Department { get; set; }
    }
}
