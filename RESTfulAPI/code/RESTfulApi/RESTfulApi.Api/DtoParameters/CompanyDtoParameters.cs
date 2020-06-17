using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.DtoParameters
{
    public class CompanyDtoParameters
    {
        //public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string SearchTerm { get; set; }
    }
}
