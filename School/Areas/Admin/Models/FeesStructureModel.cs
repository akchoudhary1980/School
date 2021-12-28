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


        [Display(Name = "Fees Head Name")]
        public int FeesHeadID { get; set; }

        [Display(Name = "Fees Amount")]
        public double FeesAmount { get; set; }

        [Display(Name = "Billing Cycle")] 
        public string BillingCycle { get; set; } // According to Cycle


        [Display(Name = "Due On")]
        public DateTime DueOn { get; set; } // Month and Date 

        public int SessionYearID { get; set; }
    }
}
