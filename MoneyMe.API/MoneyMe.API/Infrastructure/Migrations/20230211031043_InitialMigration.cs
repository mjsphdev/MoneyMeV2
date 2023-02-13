using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.API.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblcustomers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerid = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValueSql: "NEWID()"),
                    amountrequired = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0.00m),
                    term = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    title = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false, defaultValue: ""),
                    firstname = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    lastname = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    birthdate = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValueSql: "01/01/1900"),
                    mobile = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false, defaultValue: ""),
                    email = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblcustomers", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "tblloans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerid = table.Column<int>(type: "int", nullable: false),
                    financeamount = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0.00m),
                    repaymentsfrom = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0.00m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblloans", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_tblloans_tblcustomers_customerid",
                        column: x => x.customerid,
                        principalTable: "tblcustomers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblcustomers_customerid",
                table: "tblcustomers",
                column: "customerid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblloans_customerid",
                table: "tblloans",
                column: "customerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblloans");

            migrationBuilder.DropTable(
                name: "tblcustomers");
        }
    }
}
