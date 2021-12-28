
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class ClassModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassID { get; set; }

        [Required(ErrorMessage = "Please Enter Class Name")]
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Display(Name = "Class Description")]
        public string ClassDescription { get; set; }

        public int SessionYearID { get; set; }
    }
}
