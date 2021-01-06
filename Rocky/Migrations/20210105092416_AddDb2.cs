using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky.Migrations
{
    public partial class AddDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Image",
                "Products",
                "Picture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Picture",
                "Products",
                "Image");
        }
    }
}
