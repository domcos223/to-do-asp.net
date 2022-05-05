using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagerApi.Migrations
{
    public partial class ChangeDatabaseColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Column",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_ColumnId",
                table: "Todo",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_Column_ColumnId",
                table: "Todo",
                column: "ColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_Column_ColumnId",
                table: "Todo");

            migrationBuilder.DropTable(
                name: "Column");

            migrationBuilder.DropIndex(
                name: "IX_Todo_ColumnId",
                table: "Todo");
        }
    }
}
