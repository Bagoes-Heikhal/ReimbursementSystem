﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using ReimbursementSystemAPI.Models;

namespace ReimbursementSystemAPI.Models

{
    [Table("tb_m_Employee")]
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }

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
        public float Salary { get; set; }

        [Required, Index(IsUnique = true)]
        public string Email { get; set; }
        public Gender Gender { get; set; }

        [JsonIgnore]
        public virtual Account Accounts { get; set; }

        [JsonIgnore]
        public virtual Reimbusment Reimbusments { get; set; }

        [JsonIgnore]
        public virtual Employee_Attachment Employee_Attachments { get; set; }

        [JsonIgnore]
        public virtual Department Departments { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public virtual Job Jobs { get; set; }
        public int JobId { get; set; }

        [JsonIgnore]
        public virtual Religion Religions { get; set; }
        public int ReligionId { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
