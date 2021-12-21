using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ReimbursementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ReimbursementSystemAPI.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Department> Departements { get; set; }
        public DbSet<Employee_Attachment> Employee_Attachment { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Reimbusment> Reimbusments { get; set; }
        public DbSet<Religion> Religions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One to One
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Accounts)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.EmployeeId);

            //One to One
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Employee_Attachments)
                .WithOne(b => b.Employees)
                .HasForeignKey<Employee_Attachment>(b => b.EmployeeId);

            //One to One
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Reimbusments)
                .WithOne(b => b.Employees)
                .HasForeignKey<Reimbusment>(b => b.EmployeeId);

            //One to One
            modelBuilder.Entity<Role>()
                .HasOne(a => a.Accounts)
                .WithOne(b => b.Roles)
                .HasForeignKey<Account>(b => b.RoleId);

            //One to many
            modelBuilder.Entity<Expense>()
                .HasMany(c => c.Reimbusments)
                .WithOne(e => e.Expenses);

            //One to many
            modelBuilder.Entity<Form>()
                .HasMany(c => c.Expenses)
                .WithOne(c => c.Forms);

            //One to many
            modelBuilder.Entity<Department>()
                .HasMany(c => c.Employees)
                .WithOne(c => c.Departements);

            //One to many
            modelBuilder.Entity<Job>()
                .HasMany(c => c.Employees)
                .WithOne(c => c.Jobs);

            //One to many
            modelBuilder.Entity<Religion>()
                .HasMany(c => c.Employees)
                .WithOne(c => c.Religions);
        }

    }

    class CustomResolver : DefaultContractResolver
    {
        private readonly List<string> _namesOfVirtualPropsToKeep = new List<string>(new String[] { });

        public CustomResolver() { }

        public CustomResolver(IEnumerable<string> namesOfVirtualPropsToKeep)
        {
            this._namesOfVirtualPropsToKeep = namesOfVirtualPropsToKeep.Select(x => x.ToLower()).ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;
            if (propInfo != null)
            {
                if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal
                    && !_namesOfVirtualPropsToKeep.Contains(propInfo.Name.ToLower()))
                {
                    prop.ShouldSerialize = obj => false;
                }
            }
            return prop;
        }
    }
}

