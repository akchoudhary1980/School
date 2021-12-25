using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class StateModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please Enter State Name")]
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Select State Type")]
        [Display(Name = "State Type")]
        public string StateType { get; set; }

        [Required(ErrorMessage = "Please Select Country Name")]
        [Display(Name = "Country Name")]
        public int CountryID { get; set; }
    }
}
