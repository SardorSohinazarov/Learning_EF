using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mapping_Strategy_TPT.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStudentTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1L, new DateTime(2003, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sardor Sohinazarov", "998912040618" },
                    { 2L, new DateTime(2010, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarvarbek Sohinazarov", "998912040619" },
                    { 3L, new DateTime(2002, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sanjarbek Sohinazarov", "998912040620" },
                    { 4L, new DateTime(1980, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Valiyev Inomjon", "998903440723" },
                    { 5L, new DateTime(1980, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nazirov Shuxratjon", "998912047322" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "EnrollmentDate", "Faculty", "StudentNumber" },
                values: new object[,]
                {
                    { 1L, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dasturiy injinering", "S54321" },
                    { 2L, new DateTime(2027, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Siyosat", "S67890" },
                    { 3L, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iqtisodiyot", "S98765" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Subject" },
                values: new object[,]
                {
                    { 4L, "Matematika" },
                    { 5L, "Fizika" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
