using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountApp.Migrations
{
    public partial class AddedPostedInOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Posted",
                table: "OrderDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Posted",
                table: "OrderDetails");
        }
    }
}
