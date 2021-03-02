using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace Address_Design.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressFormController : Controller
    {
        private readonly ILogger<AddressFormController> _logger;

        public AddressFormController(ILogger<AddressFormController> logger)
        {
            _logger = logger;
        }

        //
        // GET: /AddressForm/
        [HttpGet]
        public string[] Index()
        {
            return new string[]
            {
                "I Got the form from the server",
                "REPLACE ME"
            };
        }


        //
        // GET: /AddressForm/Get1
        [HttpGet("Get1")]
        public string[] Get1()
        {
            return new string[]
            {
                "This is a stub for a second GET",
                "REPLACE ME"
            };
        }



    }
}
