using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Areas.Admin.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }
        public string UserName { get; set; } // mobile 
        public string Password { get; set; } // Temp by Default 
        public string DisplayName { get; set; } // Name from Employe 
        public string Email { get; set; } // Email from Employee 
        public string Mobile { get; set; } // Primary key usename or mobile should be same 
        public string UserType { get; set; } // Client or Admin
        public string ReadRights { get; set; } // yes only own Data
        public string WrightRights { get; set; } // yes only own Data
        public string UserCreateRights { get; set; } // No 
        public string SettingRights { get; set; } // yes only own Data
        public string AccountStatus { get; set; } // Active / Deactive         
    }
}
