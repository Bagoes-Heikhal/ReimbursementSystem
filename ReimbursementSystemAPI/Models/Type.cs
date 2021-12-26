using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace ReimbursementSystemAPI.Models
{
    [Table("tb_m_Type")]
    public class Type
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; }


        [JsonIgnore]
        public virtual CategoryTable Categories { get; set; }
        public int CategoryId { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Category> Categories1 { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Employee> Employees { get; set; }
    }
}
