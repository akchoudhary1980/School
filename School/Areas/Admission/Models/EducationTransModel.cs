using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admission.Models
{
    public class EducationTransModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EducationTransID { get; set; }

        public int TokenEduID { get; set; } // Education

        [Display(Name = "Class Name")]
        public string ClassName{ get; set; }

        [Display(Name = "Board Name")]
        public string Board { get; set; }

        [Display(Name = "Passing Year")]
        public string PassingYear { get; set; }

        [Display(Name = "Total Mark")]
        public string TotalMark { get; set; }       

        [Display(Name = "Recieved Mark")]
        public string RecievedMark { get; set; }

        [Display(Name = "Percent/Grade)]
        public string PercentGrade { get; set; }
        public int SessionYearID { get; set; }
        public int ClassID { get; set; }

    }
}
