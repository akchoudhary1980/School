using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using School.Areas.Admin.Models;

namespace School.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup
        public DBContext db = new DBContext();
        private static IHostEnvironment Environment;
        public SetupController(IHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public ActionResult DatabaseForm()
        {
            return RedirectToAction("SetupProcess", "Setup");
        }
        public ActionResult SetupProcess()
        {
            string path = Environment.ContentRootPath;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "/App_Data/SetupModel.xml");
            XmlNodeList setup = xDoc.GetElementsByTagName("Setup");
            string Setup = setup[0].InnerText;

            if (Setup.Equals("No"))
            {
                // code Here  
                InsertUser();
                InsertCity();
                InsertState();
                InsertCountry();
                // Reset the sml file
                XmlDocument xDoc1 = new XmlDocument();
                xDoc1.Load(path + "/App_Data/SetupModel.xml");
                xDoc1.SelectSingleNode("SetupStatus/Setup").InnerText = "Yes";
                xDoc1.Save(path + "/App_Data/SetupModel.xml");
            }
            // Reset the session          
            return RedirectToAction("Index", "Home");
        }
        public void InsertUser()
        {
            // remove all records
            var all = db.UserModels.ToList();
            db.UserModels.RemoveRange(all);
            db.SaveChanges();

            UserModel u = new UserModel
            {
                UserID = 1,
                UserName = "9039914956",
                Mobile = "9039914956",
                Password = "Temp",
                DisplayName = "Anil Choudhary",
                Email = "akchoudhary1980@gmail.com",
                UserType = "Admin",
                AccountStatus = "Active",
                ReadRights = true,
                WrightRights = true,
                UserCreateRights = true
            };
            db.UserModels.Add(u);
            db.SaveChanges();
        }
        public void InsertCountry()
        {
            // remove all records
            var all = db.CountryModels.ToList(); // from c in dataDb.Table select c;
            db.CountryModels.RemoveRange(all);
            db.SaveChanges();
            // insert in bulk
            using (var db = new DBContext())
            {
                var country = GetCountryList();
                db.CountryModels.AddRange(country);
                db.SaveChanges();
            }
        }
        public void InsertState()
        {
            // remove all records
            var all = db.StateModels.ToList(); // from c in dataDb.Table select c;
            db.StateModels.RemoveRange(all);
            db.SaveChanges();

            // insert in bulk
            using (var db = new DBContext())
            {
                var state = GetStateList();
                db.StateModels.AddRange(state);
                db.SaveChanges();
            }
        }
        public void InsertCity()
        {
            // remove all records
            var all = db.CityModels.ToList(); // from c in dataDb.Table select c;
            db.CityModels.RemoveRange(all);
            db.SaveChanges();

            // insert in bulk
            using (var db = new DBContext())
            {
                var city = GetCityList();
                db.CityModels.AddRange(city);
                db.SaveChanges();
            }
        }
        public static List<AdmissionModel> GetCityList()
        {
            List<AdmissionModel> citylist = new List<AdmissionModel>();
            DataTable dt = ReadXML("City");
            foreach (DataRow dr in dt.Rows)
            {
                AdmissionModel c = new AdmissionModel
                {
                    CityID = Convert.ToInt32(dr["CityID"]),
                    CityName = dr["CityName"].ToString(),
                    StateID = Convert.ToInt32(dr["StateID"].ToString())

                };
                citylist.Add(c);
            }
            return citylist;
        }
        public static List<StateModel> GetStateList()
        {
            List<StateModel> statelist = new List<StateModel>();
            DataTable dt = ReadXML("State");
            foreach (DataRow dr in dt.Rows)
            {
                StateModel s = new StateModel
                {
                    StateID = Convert.ToInt32(dr["StateID"]),
                    StateName = dr["StateName"].ToString(),
                    StateType = dr["StateType"].ToString(),
                    CountryID = Convert.ToInt32(dr["CountryID"].ToString())
                };
                statelist.Add(s);
            }
            return statelist;
        }
        public static List<CountryModel> GetCountryList()
        {
            List<CountryModel> countrylist = new List<CountryModel>();
            DataTable dt = ReadXML("Country");
            foreach (DataRow dr in dt.Rows)
            {
                CountryModel c = new CountryModel
                {
                    CountryID = Convert.ToInt32(dr["CountryID"]),
                    CountryName = dr["CountryName"].ToString(),
                    Region = dr["Region"].ToString()
                };
                countrylist.Add(c);
            }
            return countrylist;
        }
        public static DataTable ReadXML(string file)
        {
            string path = Environment.ContentRootPath;
            DataSet ds = new DataSet();
            ds.ReadXml(path + "/App_Data/" + file + ".xml");
            ds.Dispose();
            return ds.Tables[0];
        }
    }
}
