using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Address_Design.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using javax.jws;

namespace Address_Design.Controllers
{
    [Route("AddressForm")]
    [ApiController]
    public class AddressFormController : Controller
    {

        public AllForms cachedForms { set; get; }

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
            String result = "";
            string connStr = "server=127.0.0.1;user=root;database=addresses;port=3306;password=usingMYSQL1!";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = constructQuery(incomingAddress);
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                if (rdr.HasRows) {
                    while (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            result += rdr.GetValue(i).ToString();
                            result += " ";
                            Console.WriteLine(rdr.GetValue(i).ToString());
                        }
                    }

                    rdr.Close();
                }
                else
                {
                    result = "There were no matching addresses found!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
            var test = Json(result);
            return Json(result);
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
        private string constructQuery(Address incomingAddress)
        {
            string json = JsonConvert.SerializeObject(incomingAddress);
            //Console.WriteLine(json);
            JObject jsonObj = JObject.Parse(json);

            IList<string> keys = jsonObj.Properties().Select(p => p.Name).ToList();
            
            string sql = "SELECT ";

            foreach (string key in keys)
            {
                var passedValue = jsonObj.GetValue(key) is null ? null: jsonObj.GetValue(key).ToString();
                if (!passedValue.Equals("")) 
                {
                    sql += "g." + key + ", ";
                }
            }
            sql = sql.Remove(sql.LastIndexOf(","));
            sql += " FROM global_addresses g WHERE ";
            foreach (string key in keys)
            {
                var passedValue = jsonObj.GetValue(key) is null ? null : jsonObj.GetValue(key).ToString();
                if (!passedValue.Equals("") && !passedValue.Equals(" "))
                {
                    sql += "g." + key + "='";
                    sql += jsonObj.GetValue(key) + "' AND ";
                }
            }
            sql = sql.Remove(sql.LastIndexOf("AND"));
            sql = sql.TrimEnd();
            sql += ";";
            Console.WriteLine(sql);

            return sql;
        }

        //private Boolean saveAddress(Address incomingAddress)
        //{
        //    if (incomingAddress.Street is null)
        //    {
        //        // do something...
        //        return false;
        //    }
        //    else
        //    {
        //        // do something positive!
        //        return true;
        //    }
        //}
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

        [HttpGet("CountryFormatReturn")]
        public ArrayList CountryFormatReturn(String country)
        {
            cachedForms = JsonConvert.DeserializeObject<AllForms>(System.IO.File.ReadAllText(@"..\Address_Design\Data\Forms.json")); //TODO Replace file with server response
            ArrayList result = new ArrayList();
            bool isExist = false;

            if (cachedForms != null)
            {
                foreach (var form in cachedForms.forms)
                {
                    if (form.country == country)
                    {
                        for (int i = 0; i < form.fields.Count; i++)
                        {
                            result.Add(form.fields[i].name);
                            isExist = true;
                        }
                    }

                    if (isExist)
                    {
                        break;
                    }

                }
            }

            return result;
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

