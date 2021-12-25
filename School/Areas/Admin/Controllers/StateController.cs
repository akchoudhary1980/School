using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace School.Areas.Admin.Controllers
{
    public class StateController : Controller
    {
        public DBContext db = new DBContext();
        public IActionResult Index()
        {
            ViewData["PageTitle"] = "State List";
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
            if (sortColumn == "StateID")
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
                var citylist = (from t1 in db.StateModels // state                                                                    
                                join t2 in db.CountryModels // country 
                                on t1.CountryID equals t2.CountryID
                                select new
                                {
                                    t1.StateID,
                                    t1.StateName,
                                    t1.StateType,
                                    t2.CountryName,
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
                    citylist = citylist.Where(m => m.StateName.Contains(searchValue)
                                                        || m.StateType.Contains(searchValue)
                                                        || m.CountryName.Contains(searchValue)
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
            ViewData["PageTitle"] = "New State";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StateModel obj)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = db.StateModels.Any(x => x.StateName == obj.StateName);
                if (duplicate)
                {
                    ModelState.AddModelError("StateName", "Duplicate Record Found");
                    return View();
                }
                else
                {
                    int incid = db.StateModels.DefaultIfEmpty().Max(r => r == null ? 0 : r.StateID);
                    obj.StateID = incid + 1;

                    db.StateModels.Add(obj);
                    db.SaveChanges();
                    HttpContext.Session.SetString("Create", "Yes");
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
            ViewData["PageTitle"] = "Edit State";
            var model = db.StateModels.Where(x => x.StateID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(StateModel obj)
        {
            if (ModelState.IsValid)
            {
                // Check Duplicate and prevet duplication at the time of edit 
                DBContext db1 = new DBContext();
                var oldvalue = db1.StateModels.Where(x => x.StateID == obj.StateID).SingleOrDefault();
                if (oldvalue.StateName != obj.StateName)
                {
                    bool duplicate = db1.StateModels.Any(x => x.StateName == obj.StateName);
                    if (duplicate)
                    {
                        ModelState.AddModelError("StateName", "Duplicate Record Found");
                        return View();
                    }
                    else
                    {

                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();
                        HttpContext.Session.SetString("Edit", "Yes");
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {

                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    HttpContext.Session.SetString("Edit", "Yes");
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
            ViewData["PageTitle"] = "Delete State";
            var model = db.StateModels.Where(x => x.StateID == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id, string confirm)
        {
            string s = confirm;
            if (confirm == "Yes")
            {
                db.StateModels.RemoveRange(db.StateModels.Where(x => x.StateID == id));
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public static List<SelectListItem> GetCountryList()
        {
            DBContext db1 = new DBContext();
            List<SelectListItem> ls = new List<SelectListItem>();
            var mylist = db1.CountryModels.ToList();
            ls.Add(new SelectListItem() { Text = "Select", Value = "" });
            foreach (var temp in mylist)
            {
                ls.Add(new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
            }
            db1.Dispose();
            return ls;
        }

        public static List<SelectListItem> GetStateTypeList()
        {
            List<SelectListItem> ls = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Select", Value = "" },
                new SelectListItem() { Text = "State", Value = "S" },
                new SelectListItem() { Text = "Union Teritery", Value = "T" }
            };
            return ls;
        }
    }
}
