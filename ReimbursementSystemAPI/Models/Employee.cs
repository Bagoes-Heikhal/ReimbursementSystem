using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models

{
    [Table("tb_m_Employee")]
    public class Employee
    {
        [Key]
        public string Employee_Id { get; set; }

        [Required, Index(IsUnique = true)]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required, Index(IsUnique = true)]
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }

        [Required, Index(IsUnique = true)]
        public string Email { get; set; }
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
