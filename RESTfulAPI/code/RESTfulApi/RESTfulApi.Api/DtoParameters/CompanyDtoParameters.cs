using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string OrderBy { get; set; } = "CompanyName";

        public string Fields { get; set; }
    }
}
