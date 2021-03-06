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
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Address_Design.Controllers
{
    [Route("AddressForm")]
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
            string connStr = "server=127.0.0.1;user=root;database=addresses;port=3306;password=usingMYSQL1!";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT g.Recipient, g.AddressLine, g.City, g.State, g.ZipCode FROM global_addresses g WHERE g.AddressLine = '123 Main St.'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.WriteLine(rdr.GetValue(i).ToString());
                    }
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");

            return Json(status);
        }

        //[HttpPost]
        //public JsonResult PostAddress(Address incomingAddress)
        //{
        //    Address status = null;
        //    try
        //    {
        //        saveAddress(incomingAddress);
        //        status = new Address() { Street = "main street", ZipCode = "9805" };
        //    }
        //    catch (Exception e)
        //    {
        //        new NotImplementedException();
        //    }

        //    return Json(status);
        //}

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
            //TODO plug int server call here, store response as jsonResponse or String 

            cachedForms = JsonConvert.DeserializeObject<AllForms>(System.IO.File.ReadAllText(@"..\Address_Design\Data\Forms.json")); //TODO Replace file with server response

            ViewBag.cachedForms = cachedForms;

            return cachedForms;
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

