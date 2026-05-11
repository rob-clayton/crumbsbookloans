using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace CrumbsBookLoans.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Borrower", "Isbn", "LoanStatus", "Owner", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1,  "C.J. Cherryh",       null,      "978-0879977382", 0, "Rob",     new DateOnly(1981,  9, 1), "Downbelow Station" },
                    { 2,  "C.J. Cherryh",       "Lukey",   "978-0446364638", 1, "Jess",    new DateOnly(1988,  6, 1), "Cyteen" },
                    { 3,  "C.J. Cherryh",       null,      "978-0886776107", 0, "Geordie", new DateOnly(1994,  1, 1), "Foreigner" },
                    { 4,  "C.J. Cherryh",       null,      "978-0879977542", 0, "Damo",    new DateOnly(1982,  6, 1), "The Pride of Chanur" },
                    { 5,  "C.J. Cherryh",       "Liam",    "978-0879977252", 1, "Lukey",   new DateOnly(1976,  1, 1), "Gate of Ivrel" },
                    { 6,  "C.J. Cherryh",       null,      "978-0879977153", 0, "Liam",    new DateOnly(1980,  1, 1), "Serpent's Reach" },
                    { 7,  "C.J. Cherryh",       null,      "978-0879977566", 0, "David",   new DateOnly(1982,  1, 1), "Merchanter's Luck" },
                    { 8,  "C.J. Cherryh",       "Rob",     "978-0446364621", 1, "Bill",    new DateOnly(1989,  1, 1), "Rimrunners" },
                    { 9,  "C.J. Cherryh",       null,      "978-0446364614", 0, "Dave",    new DateOnly(1991,  1, 1), "Heavy Time" },
                    { 10, "C.J. Cherryh",       null,      "978-0879977016", 0, "Hana",    new DateOnly(1976,  1, 1), "Brothers of Earth" },
                    { 11, "Diana Wynne Jones",  null,      "978-0061447242", 0, "Rob",     new DateOnly(1986,  4, 1), "Howl's Moving Castle" },
                    { 12, "Diana Wynne Jones",  "Geordie", "978-0064473897", 1, "Jess",    new DateOnly(1984,  9, 1), "Fire and Hemlock" },
                    { 13, "Diana Wynne Jones",  null,      "978-0061479946", 0, "Geordie", new DateOnly(1977,  5, 1), "Charmed Life" },
                    { 14, "Diana Wynne Jones",  null,      "978-0064473941", 0, "Damo",    new DateOnly(1988,  1, 1), "The Lives of Christopher Chant" },
                    { 15, "Diana Wynne Jones",  null,      "978-0064473934", 0, "Lukey",   new DateOnly(1982,  1, 1), "Witch Week" },
                    { 16, "Diana Wynne Jones",  "David",   "978-0064473927", 1, "Liam",    new DateOnly(1980,  1, 1), "The Magicians of Caprona" },
                    { 17, "Diana Wynne Jones",  null,      "978-0061447259", 0, "David",   new DateOnly(1990,  1, 1), "Castle in the Air" },
                    { 18, "Diana Wynne Jones",  null,      "978-0061020261", 0, "Bill",    new DateOnly(1997,  1, 1), "Deep Secret" },
                    { 19, "Diana Wynne Jones",  null,      "978-0064407588", 0, "Dave",    new DateOnly(1987,  1, 1), "A Tale of Time City" },
                    { 20, "Diana Wynne Jones",  "Rob",     "978-0064473880", 1, "Hana",    new DateOnly(1975,  1, 1), "Dogsbody" },
                    { 21, "Bob Shaw",           null,      "978-0575016972", 0, "Rob",     new DateOnly(1972,  1, 1), "Other Days, Other Eyes" },
                    { 22, "Bob Shaw",           null,      "978-0575018297", 0, "Jess",    new DateOnly(1975,  1, 1), "Orbitsville" },
                    { 23, "Bob Shaw",           "Damo",    "978-0575038233", 1, "Geordie", new DateOnly(1986,  1, 1), "The Ragged Astronauts" },
                    { 24, "Bob Shaw",           null,      "978-0575016965", 0, "Damo",    new DateOnly(1967,  1, 1), "Night Walk" },
                    { 25, "Bob Shaw",           null,      "978-0575016958", 0, "Lukey",   new DateOnly(1968,  1, 1), "The Two-Timers" },
                    { 26, "Bob Shaw",           "Bill",    "978-0575021617", 1, "Liam",    new DateOnly(1976,  1, 1), "A Wreath of Stars" },
                    { 27, "Bob Shaw",           null,      "978-0575024359", 0, "David",   new DateOnly(1977,  1, 1), "Who Goes Here?" },
                    { 28, "Bob Shaw",           null,      "978-0575033634", 0, "Bill",    new DateOnly(1983,  1, 1), "Orbitsville Departure" },
                    { 29, "Bob Shaw",           null,      "978-0575042100", 0, "Dave",    new DateOnly(1988,  1, 1), "The Wooden Spaceships" },
                    { 30, "Bob Shaw",           null,      "978-0575025523", 0, "Hana",    new DateOnly(1978,  1, 1), "Vertigo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 });
        }
    }
}
