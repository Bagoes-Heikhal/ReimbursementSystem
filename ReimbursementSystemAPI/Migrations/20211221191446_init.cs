using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Form", x => x.FormId);
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
                name: "tb_m_Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Approver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_tb_m_Expense_tb_m_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "tb_m_Form",
                        principalColumn: "FormId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ReligionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_tb_m_Employee_tb_t_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "tb_t_Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_Employee_tb_t_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "tb_t_Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_Employee_tb_t_Religion_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "tb_t_Religion",
                        principalColumn: "ReligionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Account",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_tb_m_Account_tb_t_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_t_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_Reimbusment",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Submitted_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Reimbusment", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_tb_m_Reimbusment_tb_m_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_Reimbusment_tb_m_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "tb_m_Expense",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_tb_t_EmployeeAttachment_tb_m_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_m_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Account_RoleId",
                table: "tb_m_Account",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Employee_DepartmentId",
                table: "tb_m_Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Employee_JobId",
                table: "tb_m_Employee",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Employee_ReligionId",
                table: "tb_m_Employee",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Expense_FormId",
                table: "tb_m_Expense",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Reimbusment_ExpenseId",
                table: "tb_m_Reimbusment",
                column: "ExpenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_Account");

            migrationBuilder.DropTable(
                name: "tb_m_Reimbusment");

            migrationBuilder.DropTable(
                name: "tb_t_EmployeeAttachment");

            migrationBuilder.DropTable(
                name: "tb_t_Role");

            migrationBuilder.DropTable(
                name: "tb_m_Expense");

            migrationBuilder.DropTable(
                name: "tb_m_Employee");

            migrationBuilder.DropTable(
                name: "tb_m_Form");

            migrationBuilder.DropTable(
                name: "tb_t_Department");

            migrationBuilder.DropTable(
                name: "tb_t_Job");

            migrationBuilder.DropTable(
                name: "tb_t_Religion");
        }
    }
}
