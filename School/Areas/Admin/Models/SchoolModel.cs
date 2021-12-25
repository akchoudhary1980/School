using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class SchoolModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string WhatsApp { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }       
        public string OwnerName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactMobile { get; set; }
        public string SchoolType { get; set; } // Govt. or Private
        public DateTime? EstablishedYear { get; set; }
        public string ClassFromTo { get; set; } // 1 To 8
        public int BoardID { get; set; } // Mp Board // CBSE       
        public string SchoolBoysOrGirls { get; set; } // Single , Dobule Mix
        public string SchoolAbout { get; set; }

        public string SchoolCampusArea { get; set; }
        public string SchoolBuildArea { get; set; }
        public string SchoolGroundArea { get; set; } 

        public int? SchoolNoOfClassRoom { get; set; }
        public int? SchoolNoOfLabRoom { get; set; }
        public int? SchoolNoOfToilets { get; set; }
        public int? SchoolNoOfSwimmingPool { get; set; }

        public string SchoolTimmingFrom { get; set; }
        public string SchoolTimmingTo { get; set; }
        // suspence
        public int SessionYearID { get; set; }
    }
}
