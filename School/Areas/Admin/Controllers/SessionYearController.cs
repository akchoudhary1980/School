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
    public class SessionYearController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Session Year Manage";
            ViewData["PageName"] = "Session Year List";
            ViewData["ControllerName"] = "SessionYear";
            var model = db.SessionYearModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Session Year Manage";
            ViewData["PageName"] = "New Session Year";
            ViewData["ControllerName"] = "Session Year";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SessionYearModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.SessionYearModels.Any(x => x.SessionYearName == obj.SessionYearName);
                if (duplicate)
                {
                    ModelState.AddModelError("SessionYearName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.SessionYearModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.SessionYearID);
                    obj.SessionYearID = incid + 1;
                    db.SessionYearModels.Add(obj);
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
            ViewData["PageTitle"] = "Session Year Manage";
            ViewData["PageName"] = "Update Session Year";
            ViewData["ControllerName"] = "Session Year";
            var model = db.SessionYearModels.Where(x => x.SessionYearID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SessionYearModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.SessionYearModels.Where(x => x.SessionYearID == obj.SessionYearID).SingleOrDefault();
                if (oldvalue.SessionYearName != obj.SessionYearName)
                {
                    bool duplicate = db1.SessionYearModels.Any(x => x.SessionYearName == obj.SessionYearName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("SessionYearName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Session Year Manage";
            ViewData["PageName"] = "Delete Session Year";
            ViewData["ControllerName"] = "Session Year";
            var model = db.SessionYearModels.Where(x => x.SessionYearID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(SessionYearModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.SessionYearModels.RemoveRange(db.SessionYearModels.Where(x => x.SessionYearID == obj.SessionYearID));
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
