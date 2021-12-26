using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class TeacherModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }
        public string WhatsApp { get; set; }
        public string Email { get; set; }
        public int DesginationID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public double? Salary { get; set; }
        public string IsPF { get; set; } // Yes Or No 
        public string PFNumber { get; set; } // Yes Or No
       
        public string IsClassTeacher { get; set; } // Yes or No
        public string ClassName { get; set; } // if yes 
        public string ClassSection { get; set; } // if yes 
        public string Picture { get; set; } 
        public string ScanDocuments { get; set; } 
    }
}
