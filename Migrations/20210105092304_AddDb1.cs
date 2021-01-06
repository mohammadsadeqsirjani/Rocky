using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky.Migrations
{
    public partial class AddDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Product_ApplicationType_ApplicationTypeId",
                "Product");

            migrationBuilder.DropForeignKey(
                "FK_Product_Category_CategoryId",
                "Product");

            migrationBuilder.DropPrimaryKey(
                "PK_Product",
                "Product");

            migrationBuilder.DropPrimaryKey(
                "PK_Category",
                "Category");

            migrationBuilder.DropPrimaryKey(
                "PK_ApplicationType",
                "ApplicationType");

            migrationBuilder.RenameTable(
                "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                "ApplicationType",
                newName: "ApplicationTypes");

            migrationBuilder.RenameIndex(
                "IX_Product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                "IX_Product_ApplicationTypeId",
                table: "Products",
                newName: "IX_Products_ApplicationTypeId");

            migrationBuilder.AddPrimaryKey(
                "PK_Products",
                "Products",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_Categories",
                "Categories",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_ApplicationTypes",
                "ApplicationTypes",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Products_ApplicationTypes_ApplicationTypeId",
                "Products",
                "ApplicationTypeId",
                "ApplicationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Products_Categories_CategoryId",
                "Products",
                "CategoryId",
                "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Products_ApplicationTypes_ApplicationTypeId",
                "Products");

            migrationBuilder.DropForeignKey(
                "FK_Products_Categories_CategoryId",
                "Products");

            migrationBuilder.DropPrimaryKey(
                "PK_Products",
                "Products");

            migrationBuilder.DropPrimaryKey(
                "PK_Categories",
                "Categories");

            migrationBuilder.DropPrimaryKey(
                "PK_ApplicationTypes",
                "ApplicationTypes");

            migrationBuilder.RenameTable(
                "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                "ApplicationTypes",
                newName: "ApplicationType");

            migrationBuilder.RenameIndex(
                "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                "IX_Products_ApplicationTypeId",
                table: "Product",
                newName: "IX_Product_ApplicationTypeId");

            migrationBuilder.AddPrimaryKey(
                "PK_Product",
                "Product",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_Category",
                "Category",
                "Id");

            migrationBuilder.AddPrimaryKey(
                "PK_ApplicationType",
                "ApplicationType",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Product_ApplicationType_ApplicationTypeId",
                "Product",
                "ApplicationTypeId",
                "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Product_Category_CategoryId",
                "Product",
                "CategoryId",
                "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
