using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class QualificationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QualificationID { get; set; }
        [Required(ErrorMessage = "Please Enter Qualification Name")]
        [Display(Name = "Qualification Name")]
        public string QualificationName { get; set; }

        [Display(Name = "Qualification Name")]
        public string Remark { get; set; }
        public int SessionYearID { get; set; }
    }
}
