using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class CountryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter Country Name")]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }


        [Required(ErrorMessage = "Please Enter Country Region")]
        [Display(Name = "Country Region")]
        public string Region { get; set; }


        
    }
}
