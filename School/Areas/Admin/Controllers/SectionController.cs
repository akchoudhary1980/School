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
    public class SectionController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Section Manage";
            ViewData["PageName"] = "Section List";
            ViewData["ControllerName"] = "Section";
            var model = db.SectionModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Section Manage";
            ViewData["PageName"] = "New Section";
            ViewData["ControllerName"] = "Section";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SectionModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.SectionModels.Any(x => x.SectionName == obj.SectionName);
                if (duplicate)
                {
                    ModelState.AddModelError("SectionName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.SectionModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.SectionID);
                    obj.SectionID = incid + 1;
                    db.SectionModels.Add(obj);
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
            ViewData["PageTitle"] = "Section Manage";
            ViewData["PageName"] = "Update Section";
            ViewData["ControllerName"] = "Section";
            var model = db.SectionModels.Where(x => x.SectionID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SectionModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.SectionModels.Where(x => x.SectionID == obj.SectionID).SingleOrDefault();
                if (oldvalue.SectionName != obj.SectionName)
                {
                    bool duplicate = db1.SectionModels.Any(x => x.SectionName == obj.SectionName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("SectionName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Section Manage";
            ViewData["PageName"] = "Delete Section";
            ViewData["ControllerName"] = "Section";
            var model = db.SectionModels.Where(x => x.SectionID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(SectionModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.SectionModels.RemoveRange(db.SectionModels.Where(x => x.SectionID == obj.SectionID));
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
