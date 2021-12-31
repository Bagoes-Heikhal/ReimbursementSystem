using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class submit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_Religion");

            migrationBuilder.AddColumn<DateTime>(
                name: "Submitted",
                table: "tb_m_Expense",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "tb_m_Expense");

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
        }
    }
}
