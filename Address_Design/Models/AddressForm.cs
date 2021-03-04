using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Address_Design.Models
{
    public class Field
    {
        public string name { get; set; }
        public string Type { get; set; }
        public bool required { get; set; }
    }

    public class Form
    {
        public string country { get; set; }
        public IList<Field> fields { get; set; }
    }

    public class AllForms
    {
        public IList<Form> forms { get; set; }
    }


}

