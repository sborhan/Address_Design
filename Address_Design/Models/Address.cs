using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Address_Design.Models
{
    public class Address
    {
        public string Recipient { get; set; }
        
        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        
        public string ZipCode { get; set; }

        public string Country { get; set; }
    }
}
