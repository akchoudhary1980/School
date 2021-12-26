using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using School.Areas.Admin.Models;

namespace School.Controllers
{
    public class LoginController : Controller
    {
        public DBContext db = new DBContext();

        private readonly IHostEnvironment Environment;
        public LoginController(IHostEnvironment _environment)
        {
            Environment = _environment;
        }
        public IActionResult Index()
        {
            Response.Cookies.Append("CaptchaCode", TextLib.GetCaptcha());           
            TextLib.DrawCaptch(Request.Cookies["CaptchaCode"].ToString(), Environment.ContentRootPath);
            return View();
        }
        [HttpPost]
        public IActionResult Index(FormCollection form)
        {
            string mobile = form["Mobile"];
            string password = form["Password"];
            string captchacode = form["CaptchaCode"];
            string capCode = Request.Cookies["CaptchaCode"].ToString();
            ViewData["LoginError"] = null;

            if (capCode == captchacode)
            {
                var user = db.UserModels.Where(x => x.Mobile == mobile && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    Response.Cookies.Append("UserID",user.UserID.ToString()); // Session of user
                    Response.Cookies.Append("DisplayName", user.DisplayName);
                    Response.Cookies.Append("cLoginStatus","Yes");
                    return RedirectToAction("Index", "Dashboard");
                    //return RedirectToAction("Index", "Dashboard", new { area = "" });                   
                }
                else
                {
                    ViewData["LoginError"] = "Invalid user name or password !";
                    return View();
                }
            }
            else
            {
                ViewData["LoginError"] = "Invalid Captcha Code !";
                return View();
            }
        }
        public IActionResult Recovery()
        {
            Response.Cookies.Append("CaptchaCode", TextLib.GetCaptcha());
            TextLib.DrawCaptch(Request.Cookies["CaptchaCode"].ToString(), Environment.ContentRootPath);
            return View();
        }        
    }
}
