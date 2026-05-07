using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrumbsBookLoans.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Author = table.Column<string>(type: "TEXT", nullable: true),
                    Isbn = table.Column<string>(type: "TEXT", nullable: true),
                    PublishedDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Owner = table.Column<string>(type: "TEXT", nullable: true),
                    LoanStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Borrower = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Borrower", "Isbn", "LoanStatus", "Owner", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, "Robert C. Martin", null, "978-0132350884", 0, "Alice", new DateOnly(2008, 8, 1), "Clean Code" },
                    { 2, "David Thomas", null, "978-0135957059", 0, "Bob", new DateOnly(2019, 9, 23), "The Pragmatic Programmer" },
                    { 3, "Gang of Four", "Dave", "978-0201633610", 1, "Charlie", new DateOnly(1994, 10, 31), "Design Patterns" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
