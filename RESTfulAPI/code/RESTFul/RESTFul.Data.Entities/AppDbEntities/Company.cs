using System;
using System.Collections.Generic;
using System.Text;

namespace RESTFul.Data.Entities.AppDbEntities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }

        public string Product { get; set; }
        public string Introduction { get; set; }

        public DateTime BankruptTime { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
