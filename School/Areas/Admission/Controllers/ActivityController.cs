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
    public class ActivityController : Controller
    {        
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Activity Manage";
            ViewData["PageName"] = "Activity List";
            ViewData["ControllerName"] = "Activity";
            var model = db.ActivityModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Activity Manage";
            ViewData["PageName"] = "New Activity";
            ViewData["ControllerName"] = "Activity";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActivityModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.ActivityModels.Any(x => x.ActivityName == obj.ActivityName);
                if (duplicate)
                {
                    ModelState.AddModelError("ActivityName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.ActivityModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.ActivityID);
                    obj.ActivityID = incid + 1;
                    db.ActivityModels.Add(obj);
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
            ViewData["PageTitle"] = "Activity Manage";
            ViewData["PageName"] = "Update Activity";
            ViewData["ControllerName"] = "Activity";
            var model = db.ActivityModels.Where(x => x.ActivityID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ActivityModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.ActivityModels.Where(x => x.ActivityName == obj.ActivityName).SingleOrDefault();
                if (oldvalue.ActivityName != obj.ActivityName)
                {
                    bool duplicate = db1.SectionModels.Any(x => x.SectionName == obj.ActivityName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("ActivityName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Activity Manage";
            ViewData["PageName"] = "Delete Activity";
            ViewData["ControllerName"] = "Activity";
            var model = db.ActivityModels.Where(x => x.ActivityID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(ActivityModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.ActivityModels.RemoveRange(db.ActivityModels.Where(x => x.ActivityID == obj.ActivityID));
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
