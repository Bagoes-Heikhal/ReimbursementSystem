using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class department2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "tb_t_Department",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "tb_t_Department");
        }
    }
}
