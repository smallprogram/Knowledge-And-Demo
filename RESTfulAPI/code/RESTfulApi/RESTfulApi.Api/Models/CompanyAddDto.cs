using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Models
{
    public class CompanyAddDto
    {
        [Required(ErrorMessage = "这个{0}是必填的")]
        [MaxLength(100, ErrorMessage = "{0}的最大长度不能超过{1}")]
        [DisplayName("公司名称")]
        public string Name { get; set; }
        [StringLength(500, MinimumLength = 10, ErrorMessage = "{0}的长度范围是{2}到{1}")]
        [DisplayName("简介")]
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
