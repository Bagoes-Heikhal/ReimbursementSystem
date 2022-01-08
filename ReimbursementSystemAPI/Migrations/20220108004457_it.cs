using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class it : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Category", x => x.CategoryId);
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_EmployeeAttachment", x => x.Id);
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
                name: "tb_m_Type",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Type", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_tb_m_Type_tb_m_Category_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "tb_m_Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
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
                    Salary = table.Column<float>(type: "real", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_tb_m_Employee_tb_t_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "tb_t_Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_Employee_tb_t_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "tb_t_Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
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
                    Submitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentFinace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Receipt_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: true),
                    Payee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: true),
                    Attachments = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "tb_t_ExpenseHistory",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_ExpenseHistory", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_tb_t_ExpenseHistory_tb_m_Expense_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "tb_m_Expense",
                        principalColumn: "ExpenseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Employee_DepartmentId",
                table: "tb_m_Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Employee_JobId",
                table: "tb_m_Employee",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Expense_EmployeeId",
                table: "tb_m_Expense",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Form_ExpenseId",
                table: "tb_m_Form",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Type_CategoriesCategoryId",
                table: "tb_m_Type",
                column: "CategoriesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_ExpenseHistory_ExpenseId",
                table: "tb_t_ExpenseHistory",
                column: "ExpenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_Account");

            migrationBuilder.DropTable(
                name: "tb_m_Form");

            migrationBuilder.DropTable(
                name: "tb_m_Type");

            migrationBuilder.DropTable(
                name: "tb_t_EmployeeAttachment");

            migrationBuilder.DropTable(
                name: "tb_t_ExpenseHistory");

            migrationBuilder.DropTable(
                name: "tb_t_Role");

            migrationBuilder.DropTable(
                name: "tb_m_Category");

            migrationBuilder.DropTable(
                name: "tb_m_Expense");

            migrationBuilder.DropTable(
                name: "tb_m_Employee");

            migrationBuilder.DropTable(
                name: "tb_t_Department");

            migrationBuilder.DropTable(
                name: "tb_t_Job");
        }
    }
}
