using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class FeesHeadModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int FeesHeadID { get; set; }

        [Required(ErrorMessage = "Please Enter Fees Head Name")]
        [Display(Name = "Fees Head Name")]
        public string FeesHeadName { get; set; }

        [Display(Name = "Fees Head Type")]
        public string FeesHeadType { get; set; } // Fees head Type, One Type / Mothly 

        [Display(Name = "Description")]
        public string Description { get; set; }
        public int SessionYearID { get; set; }
    }
}
