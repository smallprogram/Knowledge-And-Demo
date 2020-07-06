using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTFul.Models.Dto
{
    public class CompanyAddWithBankruptTimeDto : CompanyAddDto
    {
        public DateTime? BankruptTime { get; set; }
    }
}
