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


        [HttpPut]
        public JsonResult PostAddress(Address incomingAddress)
        {
            String result = "";
            string sql = "";
            string connStr = "server=127.0.0.1;user=root;database=addresses;port=3306;password=usingMYSQL1!";
            ArrayList jsonResult = new ArrayList();
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                if (incomingAddress.Country.Equals(" "))
                {
                    sql = constructQuery(incomingAddress, true);
                } else
                {
                    sql = constructQuery(incomingAddress, false);
                }
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                if (rdr.HasRows) {
                    while (rdr.Read())
                    {
                        for (int i = 1; i < rdr.FieldCount; i++)
                        {
                            result += rdr.GetValue(i).ToString();
                            result += " ";
                        }
                        jsonResult.Add(result);
                        result = "";
                    }

                    rdr.Close();
                }
                else
                {
                    result = "There were no matching addresses found!";
                    jsonResult.Add(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            return Json(jsonResult);
        }

        #region privateHelpers
        private string constructQuery(Address incomingAddress, bool isGlobal)
        {
            string json = JsonConvert.SerializeObject(incomingAddress);
            JObject jsonObj = JObject.Parse(json);

            IList<string> keys = jsonObj.Properties().Select(p => p.Name).ToList();
            
            string sql = "SELECT ";

            if (!isGlobal)
            {
                foreach (string key in keys)
                {
                    var passedValue = jsonObj.GetValue(key) is null ? null : jsonObj.GetValue(key).ToString();
                    if (!passedValue.Equals(""))
                    {
                        sql += "g." + key + ", ";
                    }
                }
                sql = sql.Remove(sql.LastIndexOf(","));
            }
            else
            {
                sql += " *";
            }

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

            return sql;
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
};

