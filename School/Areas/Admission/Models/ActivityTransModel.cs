using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admission.Models
{
    public class ActivityTransModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public intActivityTransID { get; set; }

        public int TokenActID { get; set; } // Education

        [Display(Name = "Class Name")]
        public string ActivityName { get; set; }

        [Display(Name = "Place Name")]
        public string PlaceName { get; set; }

        [Display(Name = "Activity Year")]
        public string ActivityYear { get; set; }

        [Display(Name = "Any Award ?")]
        public string AnyAward { get; set; } 
        
        public int SessionYearID { get; set; }
        public int ClassID { get; set; }

    }
}
