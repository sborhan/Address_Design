using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Web;
using Address_Design.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Address_Design.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressFormController : Controller
    {

        public AllForms cachedForms;

        //public ActionResult Index()
        //{
        //    return View();
        //}
        private readonly ILogger<AddressFormController> _logger;

        public AddressFormController(ILogger<AddressFormController> logger)
        {
            _logger = logger;
        }


        //GET: /AddressForm/
        [HttpGet]
        public IActionResult Index()
        {
            GetForms(); //Call API to retrieve forms from server and cache them locally. 
            return View();
        }

        [HttpPost]
        public JsonResult PostAddress(Address incomingAddress)
        {
            Address status = null;
            try
            {
                saveAddress(incomingAddress);
                status = new Address() { Street = "main street", ZipCode = "9805" };
            }
            catch (Exception e)
            {
                new NotImplementedException();
            }

            return Json(status);
        }

        #region privateHelpers
        private Boolean saveAddress(Address incomingAddress)
        {
            if (incomingAddress.Street is null)
            {
                // do something...
                return false;
            }
            else
            {
                // do something positive!
                return true;
            }
        }
        #endregion


        //GET: /AddressForm/GetForms
        [HttpGet("GetForms")]
        public AllForms GetForms()
        {
            cachedForms = JsonConvert.DeserializeObject<AllForms>(System.IO.File.ReadAllText(@"..\Address_Design\Data\Forms.json"));

            return cachedForms;
        }

        public IActionResult Blazor()
        {
            return View("_Host");
        }

    }


    ////
    //// GET: /AddressForm/FilledForm
    //[HttpGet("FilledForm")]
    //    public IActionResult FilledForm(string name, int numTimes = 1)
    //    {
    //        ViewData["Message"] = "Hello " + name;
    //        ViewData["NumTimes"] = numTimes;

    //        return View();
    //    }


    //    //
    //    // GET: /AddressForm/Get1
    //    [HttpGet("Get1")]
    //    public string[] Get1()
    //    {
    //        return new string[]
    //        {
    //            "This is a stub for a second GET",
    //            "REPLACE ME"
    //        };
    //    }

    //    //
    //    // GET: /AddressForm/Get2?name=Rick&numtimes=4
    //    [HttpGet("Get2")]
    //    public string Get2(string name, int numTimes = 1)
    //    {
    //        return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
    //    }

    //    //
    //    // GET: /AddressForm/Get3/3?name=Rick
    //    [HttpGet("Get3")]
    //    public string Get3(string name, int ID = 1)
    //    {
    //        return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
    //    }







}
;

