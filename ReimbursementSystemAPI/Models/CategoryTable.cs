using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace ReimbursementSystemAPI.Models
{
    [Table("tb_m_Category")]
    public class CategoryTable
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Employee> Employees { get; set; }
    }
}
