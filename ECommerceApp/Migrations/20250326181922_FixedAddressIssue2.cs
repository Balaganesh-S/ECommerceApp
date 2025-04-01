using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceApp.Migrations
{
    /// <inheritdoc />
    public partial class FixedAddressIssue2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_orders_OrderId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_OrderId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_AddressId",
                table: "orders",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Address_AddressId",
                table: "orders",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Address_AddressId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_AddressId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Address_OrderId",
                table: "Address",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_orders_OrderId",
                table: "Address",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
