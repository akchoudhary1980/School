using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class FeesStructureModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int FeesStructureID { get; set; }

        [Required(ErrorMessage = "Please Enter Class Name")]
        [Display(Name = "Class Name")]
        public int ClassID { get; set; }

        [Display(Name = "Icon")]
        public string Pictures { get; set; }

        [Display(Name = "Total Fees")]
        public double TotalFees { get; set; }

        public int SessionYearID { get; set; }
    }
}
