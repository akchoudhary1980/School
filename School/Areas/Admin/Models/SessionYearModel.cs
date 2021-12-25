using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class SessionYearModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SessionYearID { get; set; }

        [Required(ErrorMessage = "Please Enter Session Year Name")]
        [Display(Name = "Session Year Name")]
        public string SessionYearName { get; set; }

        [Display(Name = "Remark")]
        public string SessionYearRemark { get; set; }
    }
}
