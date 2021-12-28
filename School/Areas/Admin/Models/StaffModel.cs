using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class StaffModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Please Enter Office Staff Name")]
        [Display(Name = "Staff Name")]
        public string Name { get; set; }

        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Display(Name = "Current Address")]
        public string CurrentAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }

        [Display(Name = "WhatsApp")]
        public string WhatsApp { get; set; }
        public string Email { get; set; }

        [Display(Name = "Desgination")]
        public int DesginationID { get; set; }
        public double? Salary { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Date of Appointment")]
        public DateTime? DateOfAppointment { get; set; }

        [Display(Name = "Is PF Employee ?")]
        public string IsPF { get; set; } // Yes Or No 
        [Display(Name = "If Yes PF Number")]
        public string PFNumber { get; set; } // Yes Or No 

        [Display(Name = "Passport Photo")]
        public string Picture { get; set; }

        [Display(Name = "Resume")]
        public string Resume { get; set; }

        [Display(Name = "Scan Certificate")]
        public string ScanDocuments { get; set; }
        public int SessionYearID { get; set; }
    }
}
