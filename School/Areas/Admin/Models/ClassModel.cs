using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    class ClassModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public string ClassOf { get; set; } // Board / CBSCE
        public string ClassSection { get; set; } // Section 
        public string ClassTeacher { get; set; }  // Teacher      
    }
}
