using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_m_Account")]
    public class Account
    {
        [Key]
        public string EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[JsonIgnore]
        //public virtual Employee Employee { get; set; }

        //[JsonIgnore]
        //public virtual Role Roles { get; set; }
        //public int RoleId { get; set; }
    }
}
