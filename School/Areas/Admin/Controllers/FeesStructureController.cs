﻿using System;
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
            ViewData["ControllerName"] = "Fees Structure";
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
                    list = list.Where(m => m.ClassName.Contains(searchValue)                                                         
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
            int num = _random.Next(1000, 100000);
            Response.Cookies.Append("Token", num.ToString());
            // End

            // Remove token data
            //if(Request.Cookies["Token"].ToString()!=null)
            //{
            //    int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            //    db.FeesStructureTransTempModels.RemoveRange(db.FeesStructureTransTempModels.Where(x => x.Tokon == token));
            //    db.SaveChanges();
            //}

            // remove all records
            //var all = db.CountryModels.ToList(); // from c in dataDb.Table select c;
            //db.CountryModels.RemoveRange(all);
            //db.SaveChanges();


            ViewData["PageTitle"] = "Staff Manage";
            ViewData["PageName"] = "New Staff";
            ViewData["ControllerName"] = "Staff";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffModel obj, IFormFile file_resume, IFormFile file_scan, IFormFile file_passport)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.StaffModels.Any(x => x.Mobile == obj.Mobile);
                if (duplicate)
                {
                    ModelState.AddModelError("Mobile", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.StaffModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.StaffID);
                    obj.StaffID = incid + 1;

                    obj.Resume = TextLib.UploadFilewithHTMLControl(file_resume, Environment.ContentRootPath, "Staff_A" + obj.StaffID);
                    obj.ScanDocuments = TextLib.UploadFilewithHTMLControl(file_scan, Environment.ContentRootPath, "Staff_B" + obj.StaffID);
                    obj.Picture = TextLib.UploadFilewithHTMLControl(file_passport, Environment.ContentRootPath, "Staff_C" + obj.StaffID);

                    db.StaffModels.Add(obj);
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
            ViewData["PageTitle"] = "Staff Manage";
            ViewData["PageName"] = "Update Staff";
            ViewData["ControllerName"] = "Staff";
            var model = db.StaffModels.Where(x => x.StaffID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(StaffModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.StaffModels.Where(x => x.StaffID == obj.StaffID).SingleOrDefault();
                if (oldvalue.Mobile != obj.Mobile)
                {
                    bool duplicate = db1.StaffModels.Any(x => x.Mobile == obj.Mobile);
                    if (duplicate)
                    {
                        ModelState.AddModelError("Mobile", "Duplicate Record Found");
                        return View();
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

        public JsonResult GetTotal()
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            double sum = db.FeesStructureTransTempModels.Where(x => x.Tokon == token).Sum(y=>y.FeesAmount);           
            return Json(sum, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}
