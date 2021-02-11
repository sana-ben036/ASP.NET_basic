using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageStudent1.Migrations
{
    public partial class SeedStudentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "CIN", "Adresse", "Filiere", "IsActive", "Nom", "Prenom" },
                values: new object[] { "ha123", "Youssoufia", 0, true, "benn", "sana" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "CIN",
                keyValue: "ha123");
        }
    }
}
