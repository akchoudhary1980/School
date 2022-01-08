using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class FeesStructureTransModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int FeesStructureTransID { get; set; }

        [Display(Name = "Fees Head")]
        public int FeesHeadID { get; set; }

        [Display(Name = "Fees Head Name")]
        public string FeesHead { get; set; }



        [Display(Name = "Fees Amount")]
        public double FeesAmount { get; set; }

        [Display(Name = "Billing Cycle")] 
        public string BillingCycle { get; set; } // According to Cycle


        [Display(Name = "Due On")]
        public DateTime DueOn { get; set; } // Month and Date 

        [Display(Name = "Tokon No")]
        public int Token { get; set; }
        public int SessionYearID { get; set; }
        public int ClassID { get; set; }
    }
}
