using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Areas.Admin.Models;

namespace School.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Country Manage";
            ViewData["PageName"] = "Country List";
            ViewData["ControllerName"] = "Country";
            return View();
        }
        [HttpPost]
        public IActionResult GetIndex()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            //default desc
            var sortColumnDir = "";
            if (sortColumn == "CountryID")
            {
                sortColumnDir = "desc";
            }
            else
            {
                sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            }

            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            // data
            using (DBContext dc = new DBContext())
            {
                var list = (from t1 in db.CountryModels // Country 
                            select new
                            {
                                t1.CountryID,
                                t1.CountryName,
                                t1.Region,
                            });


                // 
                // for Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    list = list.OrderBy(sortColumn + " " + sortColumnDir);
                }
                // for Searching 
                // searching 
                if (!string.IsNullOrEmpty(searchValue))
                {
                    list = list.Where(m => m.CountryName.Contains(searchValue)
                                                        || m.Region.Contains(searchValue)
                                                        );
                }
                //
                recordsTotal = list.Count();
                var data = list.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "Country Manage";
            ViewData["PageName"] = "New Country";
            ViewData["ControllerName"] = "Country";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CountryModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.CountryModels.Any(x => x.CountryName == obj.CountryName);
                if (duplicate)
                {
                    ModelState.AddModelError("CountryName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.CountryModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.CountryID);
                    obj.CountryID = incid + 1;

                    db.CountryModels.Add(obj);
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
            ViewData["PageTitle"] = "Country Manage";
            ViewData["PageName"] = "Edit Country";
            ViewData["ControllerName"] = "Country";
            var model = db.CountryModels.Where(x => x.CountryID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CountryModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.CountryModels.Where(x => x.CountryID == obj.CountryID).SingleOrDefault();
                if (oldvalue.CountryName != obj.CountryName)
                {
                    bool duplicate = db1.CountryModels.Any(x => x.CountryName == obj.CountryName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("CountryName", "Duplicate Record Found");
                        return View();
                    }
                    else
                    {

                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                        Response.Cookies.Append("Edit", "Yes");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {

                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    Response.Cookies.Append("Edit", "Yes");
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
            ViewData["PageTitle"] = "Country Manage";
            ViewData["PageName"] = "Delete Country";
            ViewData["ControllerName"] = "Country";
            var model = db.CountryModels.Where(x => x.CountryID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id, string confirm)
        {
            string s = confirm;
            if (confirm == "Yes")
            {
                db.CountryModels.RemoveRange(db.CountryModels.Where(x => x.CountryID == id));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public static List<SelectListItem> GetRegionList()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            ls.Add(new SelectListItem() { Text = "Asia", Value = "Asia" });
            ls.Add(new SelectListItem() { Text = "Africa", Value = "Africa" });
            return ls;
        }
    }
}
