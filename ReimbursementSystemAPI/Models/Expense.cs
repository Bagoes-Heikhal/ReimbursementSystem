using ReimbursementSystemAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_m_Expense")]
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }
        public Status Status { get; set; }
        public string Approver { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public float Total { get; set; }

        [JsonIgnore]
        public virtual Employee Employees { get; set; }
        public string EmployeeId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Form> Forms { get; set; }
    }

    public enum Status
    {
        Approved,
        Rejected,
        Canceled,
        Posted
    }

}
