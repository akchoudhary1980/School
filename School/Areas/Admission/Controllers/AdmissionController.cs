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
using School.Areas.Admission.Models;

namespace School.Areas.Admission.Controllers
{
    [Area("Admission")]
    public class AdmissionController : Controller
    {
        public DBContext db = new DBContext();
        private readonly Random _random = new Random();
        private IHostEnvironment Environment;
        public AdmissionController(IHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Admission Manage";
            ViewData["PageName"] = "Admission List";
            ViewData["ControllerName"] = "Admission";
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
            if (sortColumn == "AdmissionID")
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
                var list = (from t1 in db.AdmissionModels // Admission                                                                     
                            join t2 in db.ClassModels // Class 
                            on t1.ClassID equals t2.ClassID
                            join t3 in db.StudentModels // Student
                            on t1.StudentID equals t3.StudentID
                            select new
                            {
                                t1.AdmissionID,
                                t3.Picture,
                                t3.StudentName,
                                t2.ClassName,
                                t3.FatherName,
                                t3.Mobile,
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
                                      m.StudentName.ToString().Contains(searchValue) ||
                                      m.FatherName.ToString().Contains(searchValue) ||
                                      m.Mobile.ToString().Contains(searchValue)
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
        //loop:
        //    int Token = _random.Next(1000, 100000);
        //    bool dup = db.FeesStructureTransTempModels.Any(x => x.FeesStructureTransTempID == Token);
        //    if (dup == true)
        //    {
        //        goto loop;
        //    }
        //    else
        //    {
        //        ClearTemp(Token);
        //        Response.Cookies.Append("Token", Token.ToString());
        //    }

            ViewData["PageTitle"] = "Admission Manage";
            ViewData["PageName"] = "New Admission";
            ViewData["ControllerName"] = "Admission";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdmissionModel obj, IFormFile file_icon)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.AdmissionModels.Any(x => x.StudentID == obj.StudentID);
                if (duplicate)
                {
                    ModelState.AddModelError("StudentID", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
                    int incid = db.FeesStructureModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureID);
                    obj.FeesStructureID = incid + 1;

                    // Fees Structure Trans                    
                    var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).ToList();
                    List<FeesStructureTransModel> fst = new List<FeesStructureTransModel>();
                    int maxid = db.FeesStructureTransModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransID);
                    foreach (var l in list)
                    {
                        maxid++;
                        fst.Add(new FeesStructureTransModel
                        {
                            FeesStructureTransID = maxid,
                            TokenID = l.TokenID,
                            FeesHeadID = l.FeesHeadID,
                            FeesHead = l.FeesHead,
                            FeesAmount = l.FeesAmount,
                            BillingCycle = l.BillingCycle,
                            DueOn = l.DueOn,
                            SessionYearID = l.SessionYearID,
                            ClassID = l.ClassID
                        });
                    }

                    db.FeesStructureTransModels.AddRange(fst);
                    db.SaveChanges();

                    // fees trans save 
                    obj.SessionYearID = 1;
                    obj.TokenID = token;
                    obj.Pictures = TextLib.UploadFilewithHTMLControl(file_icon, Environment.ContentRootPath, "FeesIcon" + obj.FeesStructureID);
                    double totalfees = db.FeesStructureTransModels.Where(x => x.TokenID == token).Sum(y => y.FeesAmount);
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
            ClearTemp(model.TokenID);
            Response.Cookies.Append("Token", model.TokenID.ToString());
            // Load Data in Temp File 
            List<FeesStructureTransTempModel> ls = new List<FeesStructureTransTempModel>();
            var list = db.FeesStructureTransModels.Where(x => x.TokenID == model.TokenID).ToList();

            int maxid = db.FeesStructureTransTempModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransTempID);
            foreach (var l in list)
            {
                maxid++;
                ls.Add(new FeesStructureTransTempModel
                {
                    FeesStructureTransTempID = maxid,
                    TokenID = l.TokenID,
                    FeesHeadID = l.FeesHeadID,
                    FeesHead = l.FeesHead,
                    FeesAmount = l.FeesAmount,
                    BillingCycle = l.BillingCycle,
                    DueOn = l.DueOn,
                    SessionYearID = l.SessionYearID,
                    ClassID = l.ClassID
                });
            }

            db.FeesStructureTransTempModels.AddRange(ls);
            db.SaveChanges();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FeesStructureModel obj, IFormFile file_icon)
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
                        // Remove all data from trans file 
                        db.FeesStructureTransModels.RemoveRange(db.FeesStructureTransModels.Where(x => x.TokenID == token));
                        db.SaveChanges();
                        //

                        // Fees Structure Trans                    
                        var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).ToList();
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
                                FeesAmount = l.FeesAmount,
                                BillingCycle = l.BillingCycle,
                                DueOn = l.DueOn,
                                SessionYearID = l.SessionYearID,
                                ClassID = l.ClassID
                            });
                        }
                        DBContext db2 = new DBContext();
                        db2.FeesStructureTransModels.AddRange(fst);
                        db2.SaveChanges();

                        // Picture
                        if (TextLib.UploadFilewithHTMLControl(file_icon, Environment.ContentRootPath, "FeesIcon" + obj.FeesStructureID) != "No.png")
                        {
                            obj.Pictures = TextLib.UploadFilewithHTMLControl(file_icon, Environment.ContentRootPath, "FeesIcon" + obj.FeesStructureID);
                        }
                        else
                        {
                            obj.Pictures = oldvalue.Pictures;
                        }
                        // icon etc                        
                        obj.SessionYearID = 1;
                        obj.TokenID = token;

                        // fees trans save 
                        double totalfees = db.FeesStructureTransModels.Where(x => x.TokenID == token).Sum(y => y.FeesAmount);
                        obj.TotalFees = totalfees;

                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();

                        Response.Cookies.Append("Edit", "Yes");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {

                    int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
                    // Remove all data from trans file 
                    db.FeesStructureTransModels.RemoveRange(db.FeesStructureTransModels.Where(x => x.TokenID == token));
                    db.SaveChanges();
                    // Fees Structure Trans                    
                    var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).ToList();
                    List<FeesStructureTransModel> fst = new List<FeesStructureTransModel>();
                    int maxid = db.FeesStructureTransModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransID);
                    foreach (var l in list)
                    {
                        maxid++;
                        fst.Add(new FeesStructureTransModel
                        {
                            FeesStructureTransID = maxid,
                            TokenID = l.TokenID,
                            FeesHeadID = l.FeesHeadID,
                            FeesHead = l.FeesHead,
                            FeesAmount = l.FeesAmount,
                            BillingCycle = l.BillingCycle,
                            DueOn = l.DueOn,
                            SessionYearID = l.SessionYearID,
                            ClassID = l.ClassID
                        });
                    }

                    db.FeesStructureTransModels.AddRange(fst);
                    db.SaveChanges();

                    // Picture
                    if (TextLib.UploadFilewithHTMLControl(file_icon, Environment.ContentRootPath, "FeesIcon" + obj.FeesStructureID) != "No.png")
                    {
                        obj.Pictures = TextLib.UploadFilewithHTMLControl(file_icon, Environment.ContentRootPath, "FeesIcon" + obj.FeesStructureID);
                    }
                    else
                    {
                        obj.Pictures = oldvalue.Pictures;
                    }
                    // icon etc 
                    obj.SessionYearID = 1;
                    obj.TokenID = token;
                    // fees trans save 
                    double totalfees = db.FeesStructureTransModels.Where(x => x.TokenID == token).Sum(y => y.FeesAmount);
                    obj.TotalFees = totalfees;

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

        // Trans App 
        [HttpPost]
        public JsonResult InsertRow(FeesStructureTransTempModel obj)
        {
            int incid = db.FeesStructureTransTempModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureTransTempID);
            obj.FeesStructureTransTempID = incid + 1;
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            obj.TokenID = token;
            obj.ClassID = 1;
            obj.SessionYearID = 1;
            db.FeesStructureTransTempModels.Add(obj);
            db.SaveChanges();
            var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpPost]
        public JsonResult DeleteRow(int FeesHeadID)
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            db.FeesStructureTransTempModels.RemoveRange(db.FeesStructureTransTempModels.Where(x => x.FeesHeadID == FeesHeadID && x.TokenID == token));
            db.SaveChanges();
            var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings()); // return data 
        }
        [HttpPost]
        public JsonResult FetchRows(int Token)
        {
            var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == Token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings()); // return data 
        }
        public JsonResult UpdateRow(FeesStructureTransTempModel obj)
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            obj.TokenID = token;
            obj.ClassID = 1;
            obj.SessionYearID = 1;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
            var list = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).ToList();
            return Json(list, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult GetRow(int FeesHeadID)
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            var row = db.FeesStructureTransTempModels.Where(x => x.FeesHeadID == FeesHeadID && x.TokenID == token).SingleOrDefault();
            return Json(row, new Newtonsoft.Json.JsonSerializerSettings()); // return data 
        }
        public JsonResult GetBillingCycle(int ID)
        {
            var billingcycle = db.FeesHeadModels.Where(x => x.FeesHeadID == ID).SingleOrDefault();
            string result = billingcycle.FeesHeadType;
            return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult IsDuplicate(int ID)
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            bool result = db.FeesStructureTransTempModels.Any(x => x.FeesHeadID == ID && x.TokenID == token);
            return Json(result.ToString(), new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult GetTotal()
        {
            int token = Convert.ToInt32(Request.Cookies["Token"].ToString());
            double sum = db.FeesStructureTransTempModels.Where(x => x.TokenID == token).Sum(y => y.FeesAmount);
            return Json(sum, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public void ClearTemp(int id)
        {
            db.FeesStructureTransTempModels.RemoveRange(db.FeesStructureTransTempModels.Where(x => x.TokenID == id));
            db.SaveChanges();
        }
    }
}
