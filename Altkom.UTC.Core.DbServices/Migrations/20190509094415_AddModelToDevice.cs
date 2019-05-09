using Microsoft.EntityFrameworkCore.Migrations;

namespace Altkom.UTC.Core.DbServices.Migrations
{
    public partial class AddModelToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Devices",
                maxLength: 100,
                nullable: false,
                defaultValue: "Model nieznany");

            // migrationBuilder.Sql("update dbo.Devices set Model = 'Model nieznany' where Model is null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Devices");
        }
    }
}
