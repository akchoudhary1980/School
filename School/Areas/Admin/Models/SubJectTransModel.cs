using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class SubJectTransModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubJectTransID { get; set; }       
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }       
    }
}
