using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Design.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressFormController : ControllerBase
    {
        private readonly ILogger<AddressFormController> _logger;

        public AddressFormController(ILogger<AddressFormController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string[] Get()
        {
            return new string[]
            {
                "I Got the form from the server",
                "REPLACE ME"
            };
        }

    }
}
