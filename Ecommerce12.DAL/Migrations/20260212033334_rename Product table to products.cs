using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce12.DAL.Migrations
{
    /// <inheritdoc />
    public partial class renameProducttabletoproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Users_CreatedBy",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_categories_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTranslation_Product_ProductId",
                table: "ProductTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTranslation",
                table: "ProductTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductTranslation",
                newName: "productTranslations");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "products");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTranslation_ProductId",
                table: "productTranslations",
                newName: "IX_productTranslations_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CreatedBy",
                table: "products",
                newName: "IX_products_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "products",
                newName: "IX_products_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productTranslations",
                table: "productTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Users_CreatedBy",
                table: "products",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productTranslations_products_ProductId",
                table: "productTranslations",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Users_CreatedBy",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_categories_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_productTranslations_products_ProductId",
                table: "productTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productTranslations",
                table: "productTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.RenameTable(
                name: "productTranslations",
                newName: "ProductTranslation");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_productTranslations_ProductId",
                table: "ProductTranslation",
                newName: "IX_ProductTranslation_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_products_CreatedBy",
                table: "Product",
                newName: "IX_Product_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTranslation",
                table: "ProductTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Users_CreatedBy",
                table: "Product",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_categories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTranslation_Product_ProductId",
                table: "ProductTranslation",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
