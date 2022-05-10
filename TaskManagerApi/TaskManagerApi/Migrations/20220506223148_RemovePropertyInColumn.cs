using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerApi.Migrations
{
    public partial class RemovePropertyInColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_Column_ColumnId",
                table: "Todo");

            migrationBuilder.DropIndex(
                name: "IX_Todo_ColumnId",
                table: "Todo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Todo_ColumnId",
                table: "Todo",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_Column_ColumnId",
                table: "Todo",
                column: "ColumnId",
                principalTable: "Column",
                principalColumn: "ColumnId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
