using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class BoardModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public int BoardID { get; set; }

        [Required(ErrorMessage = "Please Enter Board Name")]
        [Display(Name = "Board Name")]
        public string BoardName { get; set; }
        [Display(Name = "Board Description")]
        public string BoardDescription { get; set; }
        public int SessionYearID { get; set; }
    }
}
