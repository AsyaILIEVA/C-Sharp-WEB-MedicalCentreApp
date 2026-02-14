using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedicalCentreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialDoctorsAndPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "FullName", "Specialty" },
                values: new object[,]
                {
                    { 1, "Dr. Ivan Petrov", "General Practitioner" },
                    { 2, "Dr. Maria Ivanova", "Cardiologist" },
                    { 3, "Dr. Georgi Dimitrov", "Dermatologist" },
                    { 4, "Dr. Elena Stoyanova", "Pediatrician" },
                    { 5, "Dr. Nikolay Hristov", "Neurologist" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "DateOfBirth", "EGN", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(1989, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "8902141234", "Alexander", "Krastev", "0897123456" },
                    { 2, new DateTime(1994, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "9406232345", "Milena", "Zlateva", "0897234567" },
                    { 3, new DateTime(1986, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "8609113456", "Todor", "Angelov", "0897345678" },
                    { 4, new DateTime(1997, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "9704024567", "Radostina", "Ilieva", "0897456789" },
                    { 5, new DateTime(1988, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "8807195678", "Borislav", "Naydenov", "0897567890" },
                    { 6, new DateTime(1995, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "9501286789", "Yana", "Kostova", "0897678901" },
                    { 7, new DateTime(1987, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "8706107890", "Plamen", "Vasilev", "0897789012" },
                    { 8, new DateTime(1993, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "9309248901", "Kristina", "Marinova", "0897890123" },
                    { 9, new DateTime(1991, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "9103179012", "Stanislav", "Rusev", "0897901234" },
                    { 10, new DateTime(1998, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "9812050123", "Gabriela", "Tsvetkova", "0897012345" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

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
        }
    }
}
