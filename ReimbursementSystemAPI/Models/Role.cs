using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_t_Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

        //[JsonIgnore]
        //public virtual Account Accounts { get; set; }
    }
}
