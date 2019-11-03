using Microsoft.EntityFrameworkCore.Migrations;

namespace Beehouse.Essentials.BeAuth.Migrations
{
    public partial class Subscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "subscription_product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscription_product_ProductId1",
                table: "subscription_product",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_product_products_ProductId1",
                table: "subscription_product",
                column: "ProductId1",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscription_product_products_ProductId1",
                table: "subscription_product");

            migrationBuilder.DropIndex(
                name: "IX_subscription_product_ProductId1",
                table: "subscription_product");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "subscription_product");
        }
    }
}
