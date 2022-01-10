using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admission.Models
{
    public class StudentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        [Display(Name = "Student Code")]
        public string StudentCode{ get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Please Enter Student Name")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }


        [Required(ErrorMessage = "Please Enter Father Name")]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please Enter Mother Name")]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }


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


        [Display(Name = "Passport Photo")]
        public string Picture { get; set; }


    }
}
