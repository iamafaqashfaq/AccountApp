using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountApp.Migrations
{
    public partial class AddedGLForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GLTrans_Customers_CustomerIDId",
                table: "GLTrans");

            migrationBuilder.DropIndex(
                name: "IX_GLTrans_CustomerIDId",
                table: "GLTrans");

            migrationBuilder.DropColumn(
                name: "CustomerIDId",
                table: "GLTrans");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "GLTrans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GLTrans_CustomerID",
                table: "GLTrans",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_GLTrans_Customers_CustomerID",
                table: "GLTrans",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GLTrans_Customers_CustomerID",
                table: "GLTrans");

            migrationBuilder.DropIndex(
                name: "IX_GLTrans_CustomerID",
                table: "GLTrans");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "GLTrans");

            migrationBuilder.AddColumn<int>(
                name: "CustomerIDId",
                table: "GLTrans",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GLTrans_CustomerIDId",
                table: "GLTrans",
                column: "CustomerIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_GLTrans_Customers_CustomerIDId",
                table: "GLTrans",
                column: "CustomerIDId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
