using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_m_Form")]
    public class Form
    {
        [Key]
        public int FormId { get; set; }
        public DateTime Receipt_Date { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public Enum Category { get; set; }
        public string Payee { get; set; }
        public string Description { get; set; }
        public float Total { get; set; }
        public string Attachments { get; set; }

        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }
    }

    public enum Category
    {
        Transportation,
        Parking,
        Medical,
        Lodging
    }

}
