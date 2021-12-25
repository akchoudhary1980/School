using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Areas.Admin.Models;

namespace School.Areas.Admin.Controllers
{
    public class CityController : Controller
    {
        public DBContext db = new DBContext();      
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "City List";
            var model = db.CityModels.ToList();
            return View(model);
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
            if (sortColumn == "CityID")
            {
                sortColumnDir = "desc";
            }
            else
            {
                sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            }

            //var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            // data
            using (DBContext dc = new DBContext())
            {
                var citylist = (from t1 in db.CityModels // City                                                                    
                                join t2 in db.StateModels // State 
                                on t1.StateID equals t2.StateID
                                select new
                                {
                                    t1.CityID,
                                    t1.CityName,
                                    t2.StateName,
                                });


                // 
                // for Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    citylist = citylist.OrderBy(sortColumn + " " + sortColumnDir);
                }
                // for Searching 
                // searching 
                if (!string.IsNullOrEmpty(searchValue))
                {
                    citylist = citylist.Where(m => m.CityName.Contains(searchValue)
                                                        || m.StateName.Contains(searchValue)
                                                        );
                }
                //
                recordsTotal = citylist.Count();
                var data = citylist.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
        }
        public IActionResult Create()
        {
            ViewData["PageTitle"] = "New City";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CityModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.CityModels.Any(x => x.CityName == obj.CityName);
                if (duplicate)
                {
                    ModelState.AddModelError("CityName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.CityModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.CityID);
                    obj.CityID = incid + 1;

                    db.CityModels.Add(obj);
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
            ViewData["PageTitle"] = "Edit City";
            var model = db.CityModels.Where(x => x.CityID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(CityModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.CityModels.Where(x => x.CityID == obj.CityID).SingleOrDefault();
                if (oldvalue.CityName != obj.CityName)
                {
                    bool duplicate = db1.CityModels.Any(x => x.CityName == obj.CityName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("CityName", "Duplicate Record Found");
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
            ViewData["PageTitle"] = "Delete City";
            var model = db.CityModels.Where(x => x.CityID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id, string confirm)
        {
            string s = confirm;
            if (confirm == "Yes")
            {
                db.CityModels.RemoveRange(db.CityModels.Where(x => x.CityID == id));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        public static List<SelectListItem> GetStateList()
        {
            DBContext db1 = new DBContext();
            List<SelectListItem> ls = new List<SelectListItem>();
            var mylist = db1.StateModels.ToList();
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var temp in mylist)
            {
                ls.Add(new SelectListItem() { Text = temp.StateName, Value = temp.StateID.ToString() });
            }
            db1.Dispose();
            return ls;
        }

    }
}
