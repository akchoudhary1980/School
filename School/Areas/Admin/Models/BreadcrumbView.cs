using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class BreadcrumbView
    {        
        public string PageTitle { get; set; }
        public string ControllerName { get; set; }
    }
}
