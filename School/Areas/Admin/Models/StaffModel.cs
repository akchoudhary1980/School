﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    class StaffModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Mobile { get; set; }
        public string WhatsApp { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public double? Salary { get; set; }
        public string IsPF { get; set; } // Yes Or No 
        public string PFNumber { get; set; } // Yes Or No 
        public string Qualification { get; set; } // Add Pulse        
        public string Designation { get; set; } // from Desgination                
    }
}
