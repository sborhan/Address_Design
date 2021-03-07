using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Address_Design.Models
{
    public class Address
    {
        public string Country { get; set; }

        public string Recipient { get; set; }

        public string AddressLine { get; set; }

        public string Unit_Apt { get; set;}

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        
        public string ZipCode { get; set; }

        public string ProvinceCode { get; set; }

        public string Province { get; set; }

        public string County { get; set; }

        public string District { get; set; }

        public string Island { get; set; }

        public string StreetSection { get; set; }

        public string Canton { get; set; }

        public string PostOfficeIdentifier { get; set; }

        public string PostalDistrict { get; set; }

        public string Locality { get; set; }

        public string BuildingName { get; set; }

        public string BuildingNumber { get; set; }

        public string SubareaNumber { get; set; }

        public string Subarea { get; set; }

        public string Prefecture { get; set; }

        public string Subdivision { get; set; }

        public string Neighborhood { get; set; }

        public string Suburb { get; set; }

        public string BP { get; set; }

        public string PostOffice { get; set; }

        public string HouseNumber { get; set; }

        public string ApartmentNumber { get; set; }

    }
}
