using System;
using System.Collections.Generic;
using System.Text;

namespace RESTFul.Services.Models
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; set; }

        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            this.DestinationProperties = destinationProperties ?? throw new ArgumentNullException(nameof(destinationProperties));
            this.Revert = revert;
        }
    }
}
