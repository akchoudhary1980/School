using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admission.Models
{
    public class PaymentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentID { get; set; }

        [Display(Name = "Payment For")]
        public int FeesHeadID { get; set; } // FeesHeadID 

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; } // Cash // Cheque // RTGS / NEFT // Electronic Media   

        [Display(Name = "Payment Date")]              
        public DateTime? PaymentDate { get; set; }

        [Required(ErrorMessage = "Please Enter Payment Amount")]
        [Display(Name = "Payment Amount")]
        public double Amount { get; set; }

        [Display(Name = "Remark")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }       

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } // R for Reciept // P for payment

        [Display(Name = "Student Name")]
        public int StudentID { get; set; }

    }
}
