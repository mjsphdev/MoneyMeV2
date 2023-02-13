using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.API.Infrastructure.Migrations
{
    public partial class EditLoanForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblloans_tblcustomers_customerid",
                table: "tblloans");

            migrationBuilder.DropIndex(
                name: "IX_tblloans_customerid",
                table: "tblloans");

            migrationBuilder.AlterColumn<string>(
                name: "customerid",
                table: "tblloans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "customerid",
                table: "tblloans",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tblloans_customerid",
                table: "tblloans",
                column: "customerid");

            migrationBuilder.AddForeignKey(
                name: "FK_tblloans_tblcustomers_customerid",
                table: "tblloans",
                column: "customerid",
                principalTable: "tblcustomers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
