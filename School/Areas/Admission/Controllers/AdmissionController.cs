using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Areas.Admission.Controllers
{
    public class AdmissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
