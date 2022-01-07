using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace School.Controllers
{
    public class ShareController : Controller
    {
        readonly DBContext db = new DBContext();
        public IActionResult Index()
        {
            return View();
        }
        // Get Country List
        public JsonResult CountryAutoComplete(string Prefix)
        {
            var list = db.CountryModels.Where(x => x.CountryName.Contains(Prefix))
                       .Select(x => new { x.CountryID, x.CountryName }).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }
        // Get State List
        public JsonResult StateAutoComplete(string Prefix)
        {
            var list = db.StateModels.Where(x => x.StateName.Contains(Prefix))
                       .Select(x => new { x.StateID, x.StateName }).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }
        // Get City List
        public JsonResult CityAutoComplete(string Prefix)
        {
            var list = db.CityModels.Where(x => x.CityName.Contains(Prefix))
                       .Select(x => new { x.CityID, x.CityName }).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }

        // Get City List
        public JsonResult FeesHeadAutoComplete(string Prefix)
        {
            var list = db.FeesHeadModels.Where(x => x.FeesHeadName.Contains(Prefix))
                       .Select(x => new { x.FeesHeadID, x.FeesHeadName }).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }

        //  Get firm Name 
        


        //  Get firm Name 
        public JsonResult GetCode(string Text)
        {
            string result = TextLib.Decrypt(Text);           
            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }
        // Payment Type
        public static List<SelectListItem> GetPaymentTypeList()
        {            
            List<SelectListItem> ls = new List<SelectListItem>();           
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            ls.Add(new SelectListItem() { Text = "Advance Payment", Value = "Advance Payment" });
            ls.Add(new SelectListItem() { Text = "Part Payment", Value = "Part Payment" });
            ls.Add(new SelectListItem() { Text = "FullPayment", Value = "FullPayment" });
            return ls;
        }
        // Payment Method
        public static List<SelectListItem> GetPaymentModeList()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            ls.Add(new SelectListItem() { Text = "Cash", Value = "Cash" });
            ls.Add(new SelectListItem() { Text = "Cheque", Value = "Cheque" });
            ls.Add(new SelectListItem() { Text = "RTGS/NEFT", Value = "RTGS/NEFT" });
            ls.Add(new SelectListItem() { Text = "Google Pay", Value = "Google Pay" });
            ls.Add(new SelectListItem() { Text = "Phone Pay", Value = "Phone Pay" });
            ls.Add(new SelectListItem() { Text = "Others", Value = "Others" });
            return ls;
        }

        // Remove String
        public static string RemoveString(string str,int len)
        {
            if(str!=null)
            {
                if (str.Length > len)
                {
                    str = str.Substring(0, len) + "...";
                    return str;
                }
                else
                {
                    return str;
                }
            }
            else
            {
                return str;
            }
            
        }
        // Anguler JS Method

        // For Anguler JS Search 
       
    }
}