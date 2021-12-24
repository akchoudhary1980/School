using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    class QualificationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QualificationID { get; set; }
        public string ExaminationName { get; set; }
        public string SpecilisedInSubject { get; set; }
        public DateTime? PassingYear { get; set; }
        public string Boards { get; set; } // Or University
        public string BoardName { get; set; } 
        public string IsGradeSystem { get; set; } // yes or No        
        public string FindGrade { get; set; }
        public string GradePercent { get; set; } 
        public double? FindMarks { get; set; }
        public double? OutofMarks { get; set; }
        public string MarksPecent { get; set; }
        public int TeacherID { get; set; }
        public int StaffID { get; set; }
    }
}
