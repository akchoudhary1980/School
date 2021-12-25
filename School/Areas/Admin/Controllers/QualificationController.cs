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
    public class QualificationController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Qualification Manage";
            ViewData["PageName"] = "Qualification List";
            ViewData["ControllerName"] = "Qualification";
            var model = db.QualificationModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Qualification Manage";
            ViewData["PageName"] = "New Qualification";
            ViewData["ControllerName"] = "Qualification";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QualificationModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.QualificationModels.Any(x => x.QualificationName == obj.QualificationName);
                if (duplicate)
                {
                    ModelState.AddModelError("QualificationName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.QualificationModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.QualificationID);
                    obj.QualificationID = incid + 1;
                    db.QualificationModels.Add(obj);
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
            ViewData["PageTitle"] = "Qualification Manage";
            ViewData["PageName"] = "Update Qualification";
            ViewData["ControllerName"] = "Qualification";
            var model = db.QualificationModels.Where(x => x.QualificationID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(QualificationModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.QualificationModels.Where(x => x.QualificationID == obj.QualificationID).SingleOrDefault();
                if (oldvalue.QualificationName != obj.QualificationName)
                {
                    bool duplicate = db1.QualificationModels.Any(x => x.QualificationName == obj.QualificationName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("QualificationName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Qualification Manage";
            ViewData["PageName"] = "Delete Qualification";
            ViewData["ControllerName"] = "Qualification";
            var model = db.QualificationModels.Where(x => x.QualificationID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(QualificationModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.QualificationModels.RemoveRange(db.QualificationModels.Where(x => x.QualificationID == obj.QualificationID));
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
