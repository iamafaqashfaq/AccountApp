using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountApp.Migrations
{
    public partial class AddedSalesBookKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleBookId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SaleBookId",
                table: "OrderDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SaleBookId",
                table: "Orders",
                column: "SaleBookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SaleBookId",
                table: "OrderDetails",
                column: "SaleBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_SaleBooks_SaleBookId",
                table: "OrderDetails",
                column: "SaleBookId",
                principalTable: "SaleBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SaleBooks_SaleBookId",
                table: "Orders",
                column: "SaleBookId",
                principalTable: "SaleBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_SaleBooks_SaleBookId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SaleBooks_SaleBookId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SaleBookId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_SaleBookId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "SaleBookId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SaleBookId",
                table: "OrderDetails");
        }
    }
}
