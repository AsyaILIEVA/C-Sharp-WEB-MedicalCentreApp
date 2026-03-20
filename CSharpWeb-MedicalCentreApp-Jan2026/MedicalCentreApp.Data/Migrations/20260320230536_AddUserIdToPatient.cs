using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalCentreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patients");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DateOfBirth", "EGN", "Email", "FirstName", "LastName", "MiddleName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1989, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "8902141234", "a.krastev@example.com", "Alexander", "Krastev", "Petrov", "0897123456" },
                    { 2, null, new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "9406232345", "zlateva548@example.com", "Milena", "Zlateva", "Georgieva", "0897234567" },
                    { 3, null, new DateTime(1986, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "8609113456", "angelov.todor@example.com", "Todor", "Angelov", "Angelov", "0897345678" },
                    { 4, null, new DateTime(1997, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "9704024567", "radostina97@example.com", "Radostina", "Ilieva", "Radeva", "0897456789" },
                    { 5, null, new DateTime(1988, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "8807195678", "parvanov_bn@example.com", "Borislav", "Naydenov", "Parvanov", "0897567890" },
                    { 6, null, new DateTime(1995, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "9501286789", "kostova@example.com", "Yana", "Kostova", "Dimitrova", "0897678901" },
                    { 7, null, new DateTime(1987, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "8706107890", "p_vasilev@example.com", "Plamen", "Vasilev", "Atanasov", "0897789012" },
                    { 8, null, new DateTime(1993, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "9309248901", "k.marinova@example.com", "Kristina", "Marinova", "Petrova", "0897890123" },
                    { 9, null, new DateTime(1991, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "9103179012", "rusev@example.com", "Stanislav", "Rusev", "Tomov", "0897901234" },
                    { 10, null, new DateTime(1998, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "9812050123", "gabi123@example.com", "Gabriela", "Tsvetkova", "Danova", "0897012345" }
                });
        }
    }
}
