using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class QualificationView
    {
        public int QualificationID { get; set; }
        public string PassingYear { get; set; }
        public int BoardID { get; set; }
        public double Percent { get; set; }
        public double TotalMark { get; set; }
        public double RecieptMark { get; set; }
        public string Result { get; set; }       
    }
}
