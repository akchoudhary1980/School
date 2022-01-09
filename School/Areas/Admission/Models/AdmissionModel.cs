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

        [Required(ErrorMessage = "Please Enter City Name")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Please Select State Name")]
        [Display(Name = "State Name")]
        public int StateID { get; set; }

    }
}
