using Microsoft.EntityFrameworkCore.Migrations;

namespace ManageStudent1.Migrations
{
    public partial class AlterStudentsData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "CIN",
                keyValue: "ha123",
                column: "Nom",
                value: "ben");

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "CIN", "Adresse", "Filiere", "IsActive", "Nom", "Prenom" },
                values: new object[] { "ha111", "Safi", 1, false, "fahmi", "karim" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "CIN",
                keyValue: "ha111");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "CIN",
                keyValue: "ha123",
                column: "Nom",
                value: "benn");
        }
    }
}
