using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountApp.Migrations
{
    public partial class AddedCustomerAndGLTrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Area = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GLTrans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TranType = table.Column<string>(type: "TEXT", nullable: false),
                    Debit = table.Column<double>(type: "REAL", nullable: false),
                    Credit = table.Column<double>(type: "REAL", nullable: false),
                    TranAmount = table.Column<double>(type: "REAL", nullable: false),
                    TranDetail = table.Column<string>(type: "TEXT", nullable: false),
                    TranDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TranDateTimeStamp = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerIDId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLTrans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GLTrans_Customers_CustomerIDId",
                        column: x => x.CustomerIDId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GLTrans_CustomerIDId",
                table: "GLTrans",
                column: "CustomerIDId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GLTrans");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
