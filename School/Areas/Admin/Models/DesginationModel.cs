using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class DesginationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DesginationID { get; set; }

        [Required(ErrorMessage = "Please Enter Desgination Name")]
        [Display(Name = "Desgination Name")]
        public string DesginationName { get; set; }

        [Display(Name = "Remark")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }        
    }
}
