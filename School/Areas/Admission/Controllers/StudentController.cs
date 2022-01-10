using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using School.Areas.Admission.Models;

namespace School.Areas.Admission.Controllers
{
    [Area("Admission")]
    public class StudentController : Controller
    {
        public DBContext db = new DBContext();
        private IHostEnvironment Environment;
        public StudentController(IHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Student Manage";
            ViewData["PageName"] = "Student List";
            ViewData["ControllerName"] = "Student";
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
            if (sortColumn == "StudentID")
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
                var list = (from t1 in db.StudentModels // state                                                                    
                           
                            select new
                            {
                                t1.StudentID,
                                t1.StudentName,
                                t1.FatherName,
                                t1.City,
                                t1.Mobile,
                                t1.WhatsApp,
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
                    list = list.Where(m => m.StudentName.Contains(searchValue)
                                                         || m.FatherName.Contains(searchValue)
                                                         || m.City.Contains(searchValue)
                                                         || m.Mobile.Contains(searchValue)
                                                         || m.WhatsApp.Contains(searchValue)
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
            ViewData["PageTitle"] = "Student Manage";
            ViewData["PageName"] = "New Student";
            ViewData["ControllerName"] = "Student";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel obj, IFormFile file_passport)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.StudentModels.Any(x => x.Mobile == obj.Mobile);
                if (duplicate)
                {
                    ModelState.AddModelError("Mobile", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.StudentModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.StudentID);
                    obj.StudentID = incid + 1;
                   
                    obj.Picture = TextLib.UploadFilewithHTMLControl(file_passport, Environment.ContentRootPath, "Student" + obj.StudentID);

                    db.StudentModels.Add(obj);
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
            ViewData["PageTitle"] = "Student Manage";
            ViewData["PageName"] = "Update Student";
            ViewData["ControllerName"] = "Student";
            var model = db.StudentModels.Where(x => x.StudentID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(StudentModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.StudentModels.Where(x => x.StudentID == obj.StudentID).SingleOrDefault();
                if (oldvalue.Mobile != obj.Mobile)
                {
                    bool duplicate = db1.StudentModels.Any(x => x.Mobile == obj.Mobile);
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
            ViewData["PageTitle"] = "Student Manage";
            ViewData["PageName"] = "Delete Student";
            ViewData["ControllerName"] = "Student";
            var model = db.StudentModels.Where(x => x.StudentID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(StudentModel obj, string confirm)
        {
            string s = confirm;
            if (confirm == "Yes")
            {
                db.StudentModels.RemoveRange(db.StudentModels.Where(x => x.StudentID == obj.StudentID));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
    }
}
