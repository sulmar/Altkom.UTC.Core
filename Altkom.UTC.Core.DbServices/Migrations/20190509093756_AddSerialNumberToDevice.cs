using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.UTC.Core.DbServices.Migrations
{
    public partial class AddSerialNumberToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SerialNumber",
                table: "Devices",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Devices");
        }
    }
}
