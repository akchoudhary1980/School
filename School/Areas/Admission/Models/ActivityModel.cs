using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admission.Models
{
    public class ActivityModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int ActivityID { get; set; }

        [Required(ErrorMessage = "Please Enter Activity Name")]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public int SessionYearID { get; set; }
    }
}
