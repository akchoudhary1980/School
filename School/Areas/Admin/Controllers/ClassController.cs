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
    public class ClassController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Class Manage";
            ViewData["PageName"] = "Class List";
            ViewData["ControllerName"] = "Class";
            var model = db.ClassModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Class Manage";
            ViewData["PageName"] = "New Class";
            ViewData["ControllerName"] = "Class";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.ClassModels.Any(x => x.ClassName == obj.ClassName);
                if (duplicate)
                {
                    ModelState.AddModelError("ClassName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.ClassModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.ClassID);
                    obj.ClassID = incid + 1;
                    db.ClassModels.Add(obj);
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
            ViewData["PageTitle"] = "Class Manage";
            ViewData["PageName"] = "Update Class";
            ViewData["ControllerName"] = "Class";
            var model = db.ClassModels.Where(x => x.ClassID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ClassModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.ClassModels.Where(x => x.ClassID == obj.ClassID).SingleOrDefault();
                if (oldvalue.ClassName != obj.ClassName)
                {
                    bool duplicate = db1.ClassModels.Any(x => x.ClassName == obj.ClassName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("ClassName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Class Manage";
            ViewData["PageName"] = "Delete Class";
            ViewData["ControllerName"] = "Class";
            var model = db.ClassModels.Where(x => x.ClassID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(ClassModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.ClassModels.RemoveRange(db.ClassModels.Where(x => x.ClassID == obj.ClassID));
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
