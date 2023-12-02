using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalAdoption.Infrastructure.Migrations
{
    public partial class CreateRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalProfiles",
                table: "AnimalProfiles");

            migrationBuilder.RenameTable(
                name: "AnimalProfiles",
                newName: "Requests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "AnimalProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalProfiles",
                table: "AnimalProfiles",
                column: "Id");
        }
    }
}
