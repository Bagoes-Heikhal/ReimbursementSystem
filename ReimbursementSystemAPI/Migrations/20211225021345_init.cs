using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_EmployeeAttachment",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STNK = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_EmployeeAttachment", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Job",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Job", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Religion",
                columns: table => new
                {
                    ReligionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Religion", x => x.ReligionId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Account",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Account", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_tb_m_Account_tb_m_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Approver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_tb_m_Expense_tb_m_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Form",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Form", x => x.FormId);
                    table.ForeignKey(
                        name: "FK_tb_m_Form_tb_m_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "tb_m_Expense",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Expense_EmployeeId",
                table: "tb_m_Expense",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Form_ExpenseId",
                table: "tb_m_Form",
                column: "ExpenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_Account");

            migrationBuilder.DropTable(
                name: "tb_m_Form");

            migrationBuilder.DropTable(
                name: "tb_t_Department");

            migrationBuilder.DropTable(
                name: "tb_t_EmployeeAttachment");

            migrationBuilder.DropTable(
                name: "tb_t_Job");

            migrationBuilder.DropTable(
                name: "tb_t_Religion");

            migrationBuilder.DropTable(
                name: "tb_t_Role");

            migrationBuilder.DropTable(
                name: "tb_m_Expense");

            migrationBuilder.DropTable(
                name: "tb_m_Employee");
        }
    }
}
