using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrumbsBookLoans.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Isbn", "PublishedDate", "Title" },
                values: new object[] { "C.J. Cherryh", "978-0879977382", new DateOnly(1981, 9, 1), "Downbelow Station" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Borrower", "Isbn", "LoanStatus", "PublishedDate", "Title" },
                values: new object[] { "C.J. Cherryh", "Eve", "978-0446364638", 1, new DateOnly(1988, 6, 1), "Cyteen" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "Borrower", "Isbn", "LoanStatus", "PublishedDate", "Title" },
                values: new object[] { "C.J. Cherryh", null, "978-0886776107", 0, new DateOnly(1994, 1, 1), "Foreigner" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Borrower", "Isbn", "LoanStatus", "Owner", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 4, "C.J. Cherryh", null, "978-0879977542", 0, "Dave", new DateOnly(1982, 6, 1), "The Pride of Chanur" },
                    { 5, "C.J. Cherryh", "Frank", "978-0879977252", 1, "Eve", new DateOnly(1976, 1, 1), "Gate of Ivrel" },
                    { 6, "C.J. Cherryh", null, "978-0879977153", 0, "Frank", new DateOnly(1980, 1, 1), "Serpent's Reach" },
                    { 7, "C.J. Cherryh", null, "978-0879977566", 0, "Grace", new DateOnly(1982, 1, 1), "Merchanter's Luck" },
                    { 8, "C.J. Cherryh", "Alice", "978-0446364621", 1, "Henry", new DateOnly(1989, 1, 1), "Rimrunners" },
                    { 9, "C.J. Cherryh", null, "978-0446364614", 0, "Isla", new DateOnly(1991, 1, 1), "Heavy Time" },
                    { 10, "C.J. Cherryh", null, "978-0879977016", 0, "Jack", new DateOnly(1976, 1, 1), "Brothers of Earth" },
                    { 11, "Diana Wynne Jones", null, "978-0061447242", 0, "Alice", new DateOnly(1986, 4, 1), "Howl's Moving Castle" },
                    { 12, "Diana Wynne Jones", "Charlie", "978-0064473897", 1, "Bob", new DateOnly(1984, 9, 1), "Fire and Hemlock" },
                    { 13, "Diana Wynne Jones", null, "978-0061479946", 0, "Charlie", new DateOnly(1977, 5, 1), "Charmed Life" },
                    { 14, "Diana Wynne Jones", null, "978-0064473941", 0, "Dave", new DateOnly(1988, 1, 1), "The Lives of Christopher Chant" },
                    { 15, "Diana Wynne Jones", null, "978-0064473934", 0, "Eve", new DateOnly(1982, 1, 1), "Witch Week" },
                    { 16, "Diana Wynne Jones", "Grace", "978-0064473927", 1, "Frank", new DateOnly(1980, 1, 1), "The Magicians of Caprona" },
                    { 17, "Diana Wynne Jones", null, "978-0061447259", 0, "Grace", new DateOnly(1990, 1, 1), "Castle in the Air" },
                    { 18, "Diana Wynne Jones", null, "978-0061020261", 0, "Henry", new DateOnly(1997, 1, 1), "Deep Secret" },
                    { 19, "Diana Wynne Jones", null, "978-0064407588", 0, "Isla", new DateOnly(1987, 1, 1), "A Tale of Time City" },
                    { 20, "Diana Wynne Jones", "Alice", "978-0064473880", 1, "Jack", new DateOnly(1975, 1, 1), "Dogsbody" },
                    { 21, "Bob Shaw", null, "978-0575016972", 0, "Alice", new DateOnly(1972, 1, 1), "Other Days, Other Eyes" },
                    { 22, "Bob Shaw", null, "978-0575018297", 0, "Bob", new DateOnly(1975, 1, 1), "Orbitsville" },
                    { 23, "Bob Shaw", "Dave", "978-0575038233", 1, "Charlie", new DateOnly(1986, 1, 1), "The Ragged Astronauts" },
                    { 24, "Bob Shaw", null, "978-0575016965", 0, "Dave", new DateOnly(1967, 1, 1), "Night Walk" },
                    { 25, "Bob Shaw", null, "978-0575016958", 0, "Eve", new DateOnly(1968, 1, 1), "The Two-Timers" },
                    { 26, "Bob Shaw", "Henry", "978-0575021617", 1, "Frank", new DateOnly(1976, 1, 1), "A Wreath of Stars" },
                    { 27, "Bob Shaw", null, "978-0575024359", 0, "Grace", new DateOnly(1977, 1, 1), "Who Goes Here?" },
                    { 28, "Bob Shaw", null, "978-0575033634", 0, "Henry", new DateOnly(1983, 1, 1), "Orbitsville Departure" },
                    { 29, "Bob Shaw", null, "978-0575042100", 0, "Isla", new DateOnly(1988, 1, 1), "The Wooden Spaceships" },
                    { 30, "Bob Shaw", null, "978-0575025523", 0, "Jack", new DateOnly(1978, 1, 1), "Vertigo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Isbn", "PublishedDate", "Title" },
                values: new object[] { "Robert C. Martin", "978-0132350884", new DateOnly(2008, 8, 1), "Clean Code" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Borrower", "Isbn", "LoanStatus", "PublishedDate", "Title" },
                values: new object[] { "David Thomas", null, "978-0135957059", 0, new DateOnly(2019, 9, 23), "The Pragmatic Programmer" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "Borrower", "Isbn", "LoanStatus", "PublishedDate", "Title" },
                values: new object[] { "Gang of Four", "Dave", "978-0201633610", 1, new DateOnly(1994, 10, 31), "Design Patterns" });
        }
    }
}
