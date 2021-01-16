using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky.Infra.Data.Persistence.Migrations
{
    public partial class AddSqftToOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sqft",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sqft",
                table: "OrderDetail");
        }
    }
}
