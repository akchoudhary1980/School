using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    class SchoolModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolCity { get; set; }
        public string SchoolState { get; set; }
        public string SchoolCountry { get; set; }
        public string SchoolTelephone { get; set; }
        public string SchoolMobile { get; set; }
        public string SchoolWhatsApp { get; set; }
        public string SchoolEmail { get; set; }
        public string SchoolWebsite { get; set; }
        public string SchoolLogo { get; set; }
        //public string SchoolLogo { get; set; }
        public string SchoolOwner { get; set; }
        public string ContactPerson { get; set; }
        public string ContactMobile { get; set; }
        public string SchoolType { get; set; } // Govt. or Private
        public DateTime? EstablishedYear { get; set; }
        public string SchoolClassFromTo { get; set; } // 1 To 8
        public string SchoolBoard { get; set; } // Mp Board // CBSE
        public string SchoolBoysOrGirls { get; set; } // Single , Dobule Mix
        public string SchoolAbout { get; set; }
        public string SchoolCampusArea { get; set; }
        public string SchoolBuildArea { get; set; }
        public string SchoolGroundArea { get; set; } 
        public int? SchoolNoOfClassRoom { get; set; }
        public int? SchoolNoOfLabRoom { get; set; }
        public int? SchoolNoOfToilets { get; set; }
        public string SchoolTimmingFrom { get; set; }
        public string SchoolTimmingTo { get; set; }
        public int SessionYearID { get; set; }
    }
}
