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
    public class FeesStructureController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "FeesStructure Manage";
            ViewData["PageName"] = "FeesStructure List";
            ViewData["ControllerName"] = "FeesStructure";
            var model = db.FeesStructureModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "FeesStructure Manage";
            ViewData["PageName"] = "New FeesStructure";
            ViewData["ControllerName"] = "FeesStructure";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FeesStructureModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.FeesStructureModels.Any(x => x.FeesStructureName == obj.FeesStructureName);
                if (duplicate)
                {
                    ModelState.AddModelError("FeesStructureName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.FeesStructureModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.FeesStructureID);
                    obj.FeesStructureID = incid + 1;
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
            ViewData["PageTitle"] = "FeesStructure Manage";
            ViewData["PageName"] = "Update FeesStructure";
            ViewData["ControllerName"] = "FeesStructure";
            var model = db.FeesStructureModels.Where(x => x.FeesStructureID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(FeesStructureModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.FeesStructureModels.Where(x => x.FeesStructureID == obj.FeesStructureID).SingleOrDefault();
                if (oldvalue.FeesStructureName != obj.FeesStructureName)
                {
                    bool duplicate = db1.FeesStructureModels.Any(x => x.FeesStructureName == obj.FeesStructureName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("FeesStructureName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "FeesStructure Manage";
            ViewData["PageName"] = "Delete FeesStructure";
            ViewData["ControllerName"] = "FeesStructure";
            var model = db.FeesStructureModels.Where(x => x.FeesStructureID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(FeesStructureModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.FeesStructureModels.RemoveRange(db.FeesStructureModels.Where(x => x.FeesStructureID == obj.FeesStructureID));
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
            var mylist = db1.ClassModels.ToList(); // Where Session ID = 
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var temp in mylist)
            {
                ls.Add(new SelectListItem() { Text = temp.ClassName, Value = temp.ClassID.ToString() });
            }
            db1.Dispose();
            return ls;
        }
    }
}
