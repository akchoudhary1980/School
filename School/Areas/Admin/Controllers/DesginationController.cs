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
    public class DesginationController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Desgination Manage";
            ViewData["PageName"] = "Desgination List";
            ViewData["ControllerName"] = "Desgination";
            var model = db.DesginationModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Desgination Manage";
            ViewData["PageName"] = "New Desgination";
            ViewData["ControllerName"] = "Desgination";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DesginationModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.DesginationModels.Any(x => x.DesginationName == obj.DesginationName);
                if (duplicate)
                {
                    ModelState.AddModelError("DesginationName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.DesginationModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.DesginationID);
                    obj.DesginationID = incid + 1;
                    db.DesginationModels.Add(obj);
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
            ViewData["PageTitle"] = "Desgination Manage";
            ViewData["PageName"] = "Update Desgination";
            ViewData["ControllerName"] = "Desgination";
            var model = db.DesginationModels.Where(x => x.DesginationID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(DesginationModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.DesginationModels.Where(x => x.DesginationID == obj.DesginationID).SingleOrDefault();
                if (oldvalue.DesginationName != obj.DesginationName)
                {
                    bool duplicate = db1.DesginationModels.Any(x => x.DesginationName == obj.DesginationName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("DesginationName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Desgination Manage";
            ViewData["PageName"] = "Delete Desgination";
            ViewData["ControllerName"] = "Desgination";
            var model = db.DesginationModels.Where(x => x.DesginationID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DesginationModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.DesginationModels.RemoveRange(db.DesginationModels.Where(x => x.DesginationID == obj.DesginationID));
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
