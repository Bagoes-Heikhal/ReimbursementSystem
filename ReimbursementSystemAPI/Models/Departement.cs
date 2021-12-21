using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    [Table("tb_t_Departement")]
    public class Departement
    {
        [Key]
        public int DepartementId { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
