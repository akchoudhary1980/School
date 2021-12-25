using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Areas.Admin.Models;


namespace School.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Subject Manage";
            ViewData["PageName"] = "Subject List";
            ViewData["ControllerName"] = "Subject";
            var model = db.SubjectModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Subject Manage";
            ViewData["PageName"] = "New Subject";
            ViewData["ControllerName"] = "Subject";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubjectModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.SubjectModels.Any(x => x.SubjectName == obj.SubjectName);
                if (duplicate)
                {
                    ModelState.AddModelError("SubjectName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.SubjectModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.SubjectID);
                    obj.SubjectID = incid + 1;
                    db.SubjectModels.Add(obj);
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
            ViewData["PageTitle"] = "Subject Manage";
            ViewData["PageName"] = "Update Subject";
            ViewData["ControllerName"] = "Subject";
            var model = db.SubjectModels.Where(x => x.SubjectID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SubjectModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.SubjectModels.Where(x => x.SubjectID == obj.SubjectID).SingleOrDefault();
                if (oldvalue.SubjectName != obj.SubjectName)
                {
                    bool duplicate = db1.SubjectModels.Any(x => x.SubjectName == obj.SubjectName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("SubjectName", "Duplicate Record Found");
                        return View();
                    }
                    else
                    {

                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                        HttpContext.Response.Cookies.Append("Edit", "Yes");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    HttpContext.Response.Cookies.Append("Edit", "Yes");
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
            ViewData["PageTitle"] = "Subject Manage";
            ViewData["PageName"] = "Delete Subject";
            ViewData["ControllerName"] = "Subject";
            var model = db.SubjectModels.Where(x => x.SubjectID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(SubjectModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.SubjectModels.RemoveRange(db.SubjectModels.Where(x => x.SubjectID == obj.SubjectID));
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
