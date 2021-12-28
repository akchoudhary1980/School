using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class SectionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SectionID { get; set; }

        [Required(ErrorMessage = "Please Enter Section Name")]
        [Display(Name = "Section Name")]
        public string SectionName { get; set; }
        [Display(Name = "Section Description")]
        public string SectionDescription { get; set; }

        public int SessionYearID { get; set; }
    }
}
