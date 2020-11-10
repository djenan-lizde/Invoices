using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoices.Web.Migrations
{
    public partial class Tax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalItemPrice",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<float>(
                name: "PriceWithTax",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PriceWithoutTax",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Tax",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceWithTax",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "PriceWithoutTax",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<float>(
                name: "TotalItemPrice",
                table: "InvoiceItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
