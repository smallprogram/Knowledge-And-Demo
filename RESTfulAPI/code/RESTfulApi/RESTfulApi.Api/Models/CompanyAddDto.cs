using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.Models
{
    public class CompanyAddDto
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
