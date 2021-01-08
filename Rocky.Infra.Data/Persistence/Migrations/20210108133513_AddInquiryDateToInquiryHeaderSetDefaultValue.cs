using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky.Infra.Data.Persistence.Migrations
{
    public partial class AddInquiryDateToInquiryHeaderSetDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InquiryDate",
                table: "InquiryHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 8, 17, 5, 13, 410, DateTimeKind.Local).AddTicks(5833),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InquiryDate",
                table: "InquiryHeader",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 1, 8, 17, 5, 13, 410, DateTimeKind.Local).AddTicks(5833));
        }
    }
}
