using Microsoft.EntityFrameworkCore.Migrations;

namespace ReimbursementSystemAPI.Migrations
{
    public partial class allownull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_ExpenseHistory_tb_m_Expense_ExpenseId",
                table: "tb_t_ExpenseHistory");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "tb_t_ExpenseHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_ExpenseHistory_tb_m_Expense_ExpenseId",
                table: "tb_t_ExpenseHistory",
                column: "ExpenseId",
                principalTable: "tb_m_Expense",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_ExpenseHistory_tb_m_Expense_ExpenseId",
                table: "tb_t_ExpenseHistory");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "tb_t_ExpenseHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_ExpenseHistory_tb_m_Expense_ExpenseId",
                table: "tb_t_ExpenseHistory",
                column: "ExpenseId",
                principalTable: "tb_m_Expense",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
