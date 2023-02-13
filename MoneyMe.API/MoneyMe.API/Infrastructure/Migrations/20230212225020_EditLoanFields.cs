using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMe.API.Infrastructure.Migrations
{
    public partial class EditLoanFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "financeamount",
                table: "tblloans",
                newName: "totalrepayment");

            migrationBuilder.AddColumn<decimal>(
                name: "establishmentfee",
                table: "tblloans",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.00m);

            migrationBuilder.AddColumn<decimal>(
                name: "totalinterest",
                table: "tblloans",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0.00m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "establishmentfee",
                table: "tblloans");

            migrationBuilder.DropColumn(
                name: "totalinterest",
                table: "tblloans");

            migrationBuilder.RenameColumn(
                name: "totalrepayment",
                table: "tblloans",
                newName: "financeamount");
        }
    }
}
