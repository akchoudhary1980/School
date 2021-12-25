using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using School.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BoardController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Board Manage";
            ViewData["PageName"] = "Board List";
            ViewData["ControllerName"] = "Board";
            var model = db.BoardModels.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "New Board";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BoardModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.BoardModels.Any(x => x.BoardName == obj.BoardName);
                if (duplicate)
                {
                    ModelState.AddModelError("BoardName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.BoardModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.BoardID);
                    obj.BoardID = incid + 1;
                    db.BoardModels.Add(obj);
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
            ViewData["PageTitle"] = "Edit Board";
            var model = db.BoardModels.Where(x => x.BoardID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BoardModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.BoardModels.Where(x => x.BoardID == obj.BoardID).SingleOrDefault();
                if (oldvalue.BoardName != obj.BoardName)
                {
                    bool duplicate = db1.BoardModels.Any(x => x.BoardName == obj.BoardName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("BoardName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Delete Board";
            var model = db.BoardModels.Where(x => x.BoardID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(BoardModel obj, string confirm)
        {
            if (confirm == "Yes")
            {
                db.BoardModels.RemoveRange(db.BoardModels.Where(x => x.BoardID == obj.BoardID));
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
