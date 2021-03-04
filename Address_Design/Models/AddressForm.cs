using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Address_Design.Models
{
    public class AddressForm
    {
        public AddressForm(string json)
        {
            JObject jObject = JObject.Parse(json);
            if (jObject.ContainsKey("country"))
            {
                this.country = (string)jObject.GetValue("country");
            }

            this.fields = jObject.GetValue("fields").ToObject<IList<Field>>();

        }

        public string country { get; set; }
        public IList<Field> fields { get; set; }

        public class Field
        {
            public string name { get; set; }
            public string Type { get; set; }
            public bool required { get; set; }
        }

    }
}

