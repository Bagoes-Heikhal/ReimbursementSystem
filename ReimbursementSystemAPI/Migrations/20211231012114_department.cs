using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_Employee_tb_t_Job_JobId",
                table: "tb_m_Employee");

            migrationBuilder.AlterColumn<float>(
                name: "Salary",
                table: "tb_m_Employee",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "tb_m_Employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "tb_m_Employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_Employee_DepartmentId",
                table: "tb_m_Employee",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_Employee_tb_t_Department_DepartmentId",
                table: "tb_m_Employee",
                column: "DepartmentId",
                principalTable: "tb_t_Department",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_Employee_tb_t_Job_JobId",
                table: "tb_m_Employee",
                column: "JobId",
                principalTable: "tb_t_Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_Employee_tb_t_Department_DepartmentId",
                table: "tb_m_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_Employee_tb_t_Job_JobId",
                table: "tb_m_Employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_Employee_DepartmentId",
                table: "tb_m_Employee");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "tb_m_Employee");

            migrationBuilder.AlterColumn<float>(
                name: "Salary",
                table: "tb_m_Employee",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "tb_m_Employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_Employee_tb_t_Job_JobId",
                table: "tb_m_Employee",
                column: "JobId",
                principalTable: "tb_t_Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
