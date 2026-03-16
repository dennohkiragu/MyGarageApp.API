using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageApp.API.Migrations
{
    /// <inheritdoc />
    public partial class LinkInvoiceItemsToLookupItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LookupItemId",
                table: "InvoiceItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_LookupItemId",
                table: "InvoiceItems",
                column: "LookupItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_LookupItems_LookupItemId",
                table: "InvoiceItems",
                column: "LookupItemId",
                principalTable: "LookupItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_LookupItems_LookupItemId",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_LookupItemId",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "LookupItemId",
                table: "InvoiceItems");
        }
    }
}
