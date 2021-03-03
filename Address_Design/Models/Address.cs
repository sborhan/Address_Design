using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Address_Design.Models
{
    public class Address
    {
        public string Street { get; set; }
        
        public string ZipCode { get; set; }
    }
}
