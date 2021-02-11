using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageStudent1.Migrations
{
    public partial class AlterStudentsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "CIN", "Adresse", "Filiere", "IsActive", "Nom", "Prenom" },
                values: new object[] { "ha456", "Safi", 1, false, "salman", "samir" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "CIN",
                keyValue: "ha456");
        }
    }
}
