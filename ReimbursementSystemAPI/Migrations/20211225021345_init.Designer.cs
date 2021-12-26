﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReimbursementSystemAPI.Models;

namespace ReimbursementSystemAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20211225021345_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Account", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("tb_m_Account");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("tb_t_Department");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.HasKey("EmployeeId");

                    b.ToTable("tb_m_Employee");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee_Attachment", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("STNK")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("tb_t_EmployeeAttachment");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Approver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.HasKey("ExpenseId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("tb_m_Expense");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("tb_t_Job");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Religion", b =>
                {
                    b.Property<int>("ReligionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReligionId");

                    b.ToTable("tb_t_Religion");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("tb_t_Role");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.ViewModel.Form", b =>
                {
                    b.Property<int>("FormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Attachments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("int");

                    b.Property<string>("Payee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Receipt_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.HasKey("FormId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("tb_m_Form");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Account", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Employee", "Employee")
                        .WithOne("Accounts")
                        .HasForeignKey("ReimbursementSystemAPI.Models.Account", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Expense", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Employee", "Employees")
                        .WithMany("Expenses")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.ViewModel.Form", b =>
                {
                    b.HasOne("ReimbursementSystemAPI.Models.Expense", "Expenses")
                        .WithMany("Forms")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Employee", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ReimbursementSystemAPI.Models.Expense", b =>
                {
                    b.Navigation("Forms");
                });
#pragma warning restore 612, 618
        }
    }
}