using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admission.Models
{
    public class AdmissionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdmissionID { get; set; }

        [Display(Name = "Date of Admission")]
        public DateTime DateOfAdmission { get; set; }


        [Required(ErrorMessage = "Please Enter Class Name")]
        [Display(Name = "Class Name")]
        public int ClassID { get; set; }

        [Required(ErrorMessage = "Please Enter Student Name")]
        [Display(Name = "Student Name")]
        public int StudentID { get; set; }

        public int TokenEduID { get; set; } // Education
        public int TokenActID { get; set; } // Activity 

    }
}
