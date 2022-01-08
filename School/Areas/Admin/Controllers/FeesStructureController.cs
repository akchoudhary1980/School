using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using School.Areas.Admin.Models;

namespace School.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeesStructureController : Controller
    {
        public DBContext db = new DBContext();
        private readonly Random _random = new Random();
        private IHostEnvironment Environment;     
        public FeesStructureController(IHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Fees Structure Manage";
            ViewData["PageName"] = "Fees Structure List";
            ViewData["ControllerName"] = "FeesStructure";

            Response.Cookies.Append("Token", "0");            
            // Remove token data
            if (Request.Cookies["Token"] != null)
            {
                int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
                db.FeesStructureTransTempModels.RemoveRange(db.FeesStructureTransTempModels.Where(x => x.Tokon == token));
                db.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public IActionResult GetIndex()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            //default desc
            var sortColumnDir = "";
            if (sortColumn == "FeesStructureID")
            {
                sortColumnDir = "desc";
            }
            else
            {
                sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            }

            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            // data
            using (DBContext dc = new DBContext())
            {
                var list = (from t1 in db.FeesStructureModels // fees structure                                                                     
                            join t2 in db.ClassModels // class 
                            on t1.ClassID equals t2.ClassID
                            select new
                            {
                                t1.FeesStructureID,
                                t1.Pictures,                               
                                t2.ClassName,
                                t1.TotalFees,
                            });


                // 
                // for Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    list = list.OrderBy(sortColumn + " " + sortColumnDir);
                }
                // for Searching 
                // searching 
                if (!string.IsNullOrEmpty(searchValue))
                {
                    list = list.Where(m => m.ClassName.Contains(searchValue) ||
                                      m.TotalFees.ToString().Contains(searchValue)                                                         
                                                         );
                }
                //
                recordsTotal = list.Count();
                var data = list.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
        }
        public IActionResult Create()
        {
            // Generate new token 
            loop:
            int num = _random.Next(1000, 100000);
            bool dup = db.FeesStructureModels.Any(x => x.Token == num);
            
            if(dup==false)
            {
                Response.Cookies.Append("Token", num.ToString());
            }
            else
            {
                goto loop;
            }            
            
            ViewData["PageTitle"] = "Fees Structure Manage";
            ViewData["PageName"] = "New Fees Structure";
            ViewData["ControllerName"] = "FeesStructure";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeesStructureModel obj, IFormFile file_icon)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.FeesStructureModels.Any(x => x.ClassID == obj.ClassID);
                if (duplicate)
                {
                    ModelState.AddModelError("ClassID", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int token = Convert.ToInt32(Request.Cookies["Token"].ToString());                    
                    int incid = db.FeesStructureModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureID);
                    obj.FeesStructureID = incid + 1;
                    obj.SessionYearID = 1;
                    obj.Token = token;                   
                    obj.Pictures = TextLib.UploadFilewithHTMLControl(file_icon, Environment.ContentRootPath, "FeesIcon" + obj.FeesStructureID);                    
                   
                    // Fees Structure Trans                    
                    var list = db.FeesStructureTransTempModels.Where(x => x.Tokon == token).ToList();
                    List<FeesStructureTransModel> fst = new List<FeesStructureTransModel>();
                    int maxid = db.FeesStructureTransModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransID);
                    foreach (var l in list)
                    {
                        maxid++;
                        fst.Add(new FeesStructureTransModel
                        {         
                            FeesStructureTransID = maxid,
                            FeesHeadID = l.FeesHeadID,
                            FeesHead = l.FeesHead,
                            FeesAmount=l.FeesAmount,
                            BillingCycle=l.BillingCycle,
                            DueOn = l.DueOn,
                            Token=l.Tokon,
                            SessionYearID = l.SessionYearID,
                            ClassID = l.ClassID
                        });
                    }
                    DBContext db1 = new DBContext();
                    db1.FeesStructureTransModels.AddRange(fst);
                    db1.SaveChanges();
                    // fees trans save 
                    double totalfees = db.FeesStructureTransModels.Where(x => x.Token == token).Sum(y => y.FeesAmount);
                    obj.TotalFees = totalfees;
                    db.FeesStructureModels.Add(obj);
                    db.SaveChanges();
                    Response.Cookies.Append("Create", "Yes");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int id)
        {
            ViewData["PageTitle"] = "Fees Structure Manage";
            ViewData["PageName"] = "Update Fees Structure";
            ViewData["ControllerName"] = "FeesStructure";
            var model = db.FeesStructureModels.Where(x => x.FeesStructureID == id).FirstOrDefault();
            // Load Data in Temp File 
            List<FeesStructureTransTempModel> ls = new List<FeesStructureTransTempModel>();
            var list = db.FeesStructureTransModels.Where(x => x.Token == model.Token).ToList();
            Response.Cookies.Append("Token", model.Token.ToString());

            int maxid = db.FeesStructureTransTempModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransTempID);
            foreach (var l in list)
            {
                maxid++;
                ls.Add(new FeesStructureTransTempModel
                {
                    FeesStructureTransTempID = maxid,
                    FeesHeadID = l.FeesHeadID,
                    FeesHead = l.FeesHead,
                    FeesAmount = l.FeesAmount,
                    BillingCycle = l.BillingCycle,
                    DueOn = l.DueOn,
                    Tokon = l.Token,
                    SessionYearID = l.SessionYearID,
                    ClassID = l.ClassID
                });
            }
            DBContext db1 = new DBContext();
            db1.FeesStructureTransTempModels.AddRange(ls);
            db1.SaveChanges();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FeesStructureModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.FeesStructureModels.Where(x => x.ClassID == obj.ClassID).SingleOrDefault();
                if (oldvalue.ClassID != obj.ClassID)
                {
                    bool duplicate = db1.FeesStructureModels.Any(x => x.ClassID == obj.ClassID);
                    if (duplicate)
                    {
                        ModelState.AddModelError("ClassID", "Duplicate Record Found");
                        return View();
                    }
                    else
                    {
                        int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
                        // Fees Structure Trans                    
                        var list = db.FeesStructureTransTempModels.Where(x => x.Tokon == token).ToList();
                        List<FeesStructureTransModel> fst = new List<FeesStructureTransModel>();
                        int maxid = db.FeesStructureTransModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransID);
                        foreach (var l in list)
                        {
                            maxid++;
                            fst.Add(new FeesStructureTransModel
                            {
                                FeesStructureTransID = maxid,
                                FeesHeadID = l.FeesHeadID,
                                FeesAmount = l.FeesAmount,
                                BillingCycle = l.BillingCycle,
                                DueOn = l.DueOn,
                                Token = l.Tokon,
                                SessionYearID = l.SessionYearID,
                                ClassID = l.ClassID
                            });
                        }
                        DBContext db2 = new DBContext();
                        db2.FeesStructureTransModels.AddRange(fst);
                        db2.SaveChanges();


                        // icon etc 
                        obj.SessionYearID = 1;
                        obj.Token = token;

                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();

                        Response.Cookies.Append("Edit", "Yes");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {

                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    Response.Cookies.Append("Edit", "Yes");
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            ViewData["PageTitle"] = "Staff Manage";
            ViewData["PageName"] = "Delete Staff";
            ViewData["ControllerName"] = "Staff";
            var model = db.StaffModels.Where(x => x.StaffID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(StaffModel obj, string confirm)
        {
            string s = confirm;
            if (confirm == "Yes")
            {
                db.StaffModels.RemoveRange(db.StaffModels.Where(x => x.StaffID == obj.StaffID));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public static List<SelectListItem> GetClassList()
        {
            DBContext db1 = new DBContext();
            List<SelectListItem> ls = new List<SelectListItem>();
            var mylist = db1.ClassModels.ToList();
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var temp in mylist)
            {
                ls.Add(new SelectListItem() { Text = temp.ClassName, Value = temp.ClassID.ToString() });
            }
            db1.Dispose();
            return ls;
        }

        
        [HttpPost]        
        public JsonResult InsertRow(FeesStructureTransTempModel obj)
        {            
            int incid = db.FeesStructureTransTempModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransTempID);
            obj.FeesStructureTransTempID = incid + 1;
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            obj.Tokon = token;
            obj.ClassID = 1;
            obj.SessionYearID = 1;
            db.FeesStructureTransTempModels.Add(obj);
            db.SaveChanges();          
            var list = db.FeesStructureTransTempModels.Where(x=>x.Tokon==token).ToList();                
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings()); 
        }
        [HttpPost]
        public JsonResult DeleteRow(int iSer)
        {
            db.FeesStructureTransTempModels.RemoveRange(db.FeesStructureTransTempModels.Where(x => x.FeesStructureTransTempID == iSer));
            db.SaveChanges();
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            var list = db.FeesStructureTransTempModels.Where(x => x.Tokon == token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings()); // return data 
        }
        [HttpPost]
        public JsonResult FetchRow(int Token)
        {   
            var list = db.FeesStructureTransTempModels.Where(x => x.Tokon == Token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings()); // return data 
        }
        public JsonResult UpdateRow(FeesStructureTransTempModel obj)
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            obj.Tokon = token;
            obj.ClassID = 1;
            obj.SessionYearID = 1;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            var list = db.FeesStructureTransTempModels.Where(x => x.Tokon == token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult GetRow(int iSer)
        {
           var row = db.FeesStructureTransTempModels.Where(x => x.FeesStructureTransTempID == iSer).SingleOrDefault();           
            return Json(row, new Newtonsoft.Json.JsonSerializerSettings()); // return data 
        }

        public JsonResult GetBillingCycle(int ID)
        {
            var billingcycle = db.FeesHeadModels.Where(x => x.FeesHeadID == ID).SingleOrDefault();
            string result = billingcycle.FeesHeadType;
            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public JsonResult IsDuplicate(int iSer)
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());            
            bool result = db.FeesStructureTransTempModels.Any(x => x.FeesHeadID == iSer && x.Tokon == token);           
            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public JsonResult GetTotal()
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            double sum = db.FeesStructureTransTempModels.Where(x => x.Tokon == token).Sum(y=>y.FeesAmount);           
            return Json(sum, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
