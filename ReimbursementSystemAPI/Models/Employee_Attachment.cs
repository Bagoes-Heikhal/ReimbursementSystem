using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_t_EmployeeAttachment")]
    public class Employee_Attachment
    {
        [Key]
        public string EmployeeId { get; set; }

        public string STNK { get; set; }

        //[JsonIgnore]
        //public virtual Employee Employees { get; set; }
    }
}
