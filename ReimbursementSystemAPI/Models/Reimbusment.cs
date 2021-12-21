using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_m_Reimbusment")]
    public class Reimbusment
    {
        [Key]
        public string EmployeeId { get; set; }
        public DateTime Submitted_Date { get; set; }

        [JsonIgnore]
        public virtual Expense Expenses { get; set; }
        public int ExpenseId { get; set; }

        public virtual Employee Employees { get; set; }

    }
}
