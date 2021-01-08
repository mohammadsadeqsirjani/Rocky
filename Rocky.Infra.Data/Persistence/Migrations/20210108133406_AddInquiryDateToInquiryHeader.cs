using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky.Infra.Data.Persistence.Migrations
{
    public partial class AddInquiryDateToInquiryHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InquiryDate",
                table: "InquiryHeader",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InquiryDate",
                table: "InquiryHeader");
        }
    }
}
