using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using School.Areas.Admin.Models;

namespace School.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeesHeadController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Fees Head Manage";
            ViewData["PageName"] = "Fees Head List";
            ViewData["ControllerName"] = "FeesHead";
            var model = db.FeesHeadModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Fees Head Manage";
            ViewData["PageName"] = "New Fees Head";
            ViewData["ControllerName"] = "FeesHead";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeesHeadModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.FeesHeadModels.Any(x => x.FeesHeadName == obj.FeesHeadName);
                if (duplicate)
                {
                    ModelState.AddModelError("FeesHeadName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.FeesHeadModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesHeadID);
                    obj.FeesHeadID = incid + 1;
                    db.FeesHeadModels.Add(obj);
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
            ViewData["PageTitle"] = "Fees Head Manage";
            ViewData["PageName"] = "Update Fees Head";
            ViewData["ControllerName"] = "FeesHead";
            var model = db.FeesHeadModels.Where(x => x.FeesHeadID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FeesHeadModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.FeesHeadModels.Where(x => x.FeesHeadID == obj.FeesHeadID).SingleOrDefault();
                if (oldvalue.FeesHeadName != obj.FeesHeadName)
                {
                    bool duplicate = db1.FeesHeadModels.Any(x => x.FeesHeadName == obj.FeesHeadName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("FeesHeadName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Fees Head Manage";
            ViewData["PageName"] = "Delete Fees Head";
            ViewData["ControllerName"] = "FeesHead";
            var model = db.FeesHeadModels.Where(x => x.FeesHeadID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(FeesHeadModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.FeesHeadModels.RemoveRange(db.FeesHeadModels.Where(x => x.FeesHeadID == obj.FeesHeadID));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public static List<SelectListItem> GetFeesHeadTypeList()
        {
            List<SelectListItem> ls = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Select", Value = "" },
                new SelectListItem() { Text = "One Time Only", Value = "One Time Only" },
                new SelectListItem() { Text = "Every Month", Value = "Every Month" },
                new SelectListItem() { Text = "Examination Fees", Value = "Examination Fees" }
            };
            return ls;
        }
    }
}
