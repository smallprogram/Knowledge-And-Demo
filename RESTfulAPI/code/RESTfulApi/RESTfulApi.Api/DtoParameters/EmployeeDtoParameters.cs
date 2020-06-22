﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulApi.Api.DtoParameters
{
    public class EmployeeDtoParameters
    {
        private const int MaxPageSize = 20;

        public string Gender { get; set; }
        public string Q { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string OrderBy { get; set; } = "Name";

    }
}
