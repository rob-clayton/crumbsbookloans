using CrumbsBookLoans.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrumbsBookLoans.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            // C.J. Cherryh
            new Book { Id = 1,  Title = "Downbelow Station",          Author = "C.J. Cherryh",       Isbn = "978-0879977382", PublishedDate = new DateOnly(1981,  9,  1), Owner = "Rob" },
            new Book { Id = 2,  Title = "Cyteen",                      Author = "C.J. Cherryh",       Isbn = "978-0446364638", PublishedDate = new DateOnly(1988,  6,  1), Owner = "Jess",    LoanStatus = LoanStatus.Borrowed, Borrower = "Lukey" },
            new Book { Id = 3,  Title = "Foreigner",                   Author = "C.J. Cherryh",       Isbn = "978-0886776107", PublishedDate = new DateOnly(1994,  1,  1), Owner = "Geordie" },
            new Book { Id = 4,  Title = "The Pride of Chanur",         Author = "C.J. Cherryh",       Isbn = "978-0879977542", PublishedDate = new DateOnly(1982,  6,  1), Owner = "Damo" },
            new Book { Id = 5,  Title = "Gate of Ivrel",               Author = "C.J. Cherryh",       Isbn = "978-0879977252", PublishedDate = new DateOnly(1976,  1,  1), Owner = "Lukey",   LoanStatus = LoanStatus.Borrowed, Borrower = "Liam" },
            new Book { Id = 6,  Title = "Serpent's Reach",             Author = "C.J. Cherryh",       Isbn = "978-0879977153", PublishedDate = new DateOnly(1980,  1,  1), Owner = "Liam" },
            new Book { Id = 7,  Title = "Merchanter's Luck",           Author = "C.J. Cherryh",       Isbn = "978-0879977566", PublishedDate = new DateOnly(1982,  1,  1), Owner = "David" },
            new Book { Id = 8,  Title = "Rimrunners",                  Author = "C.J. Cherryh",       Isbn = "978-0446364621", PublishedDate = new DateOnly(1989,  1,  1), Owner = "Bill",    LoanStatus = LoanStatus.Borrowed, Borrower = "Rob" },
            new Book { Id = 9,  Title = "Heavy Time",                  Author = "C.J. Cherryh",       Isbn = "978-0446364614", PublishedDate = new DateOnly(1991,  1,  1), Owner = "Dave" },
            new Book { Id = 10, Title = "Brothers of Earth",           Author = "C.J. Cherryh",       Isbn = "978-0879977016", PublishedDate = new DateOnly(1976,  1,  1), Owner = "Hana" },
            // Diana Wynne Jones
            new Book { Id = 11, Title = "Howl's Moving Castle",        Author = "Diana Wynne Jones",  Isbn = "978-0061447242", PublishedDate = new DateOnly(1986,  4,  1), Owner = "Rob" },
            new Book { Id = 12, Title = "Fire and Hemlock",            Author = "Diana Wynne Jones",  Isbn = "978-0064473897", PublishedDate = new DateOnly(1984,  9,  1), Owner = "Jess",    LoanStatus = LoanStatus.Borrowed, Borrower = "Geordie" },
            new Book { Id = 13, Title = "Charmed Life",                Author = "Diana Wynne Jones",  Isbn = "978-0061479946", PublishedDate = new DateOnly(1977,  5,  1), Owner = "Geordie" },
            new Book { Id = 14, Title = "The Lives of Christopher Chant", Author = "Diana Wynne Jones", Isbn = "978-0064473941", PublishedDate = new DateOnly(1988, 1, 1), Owner = "Damo" },
            new Book { Id = 15, Title = "Witch Week",                  Author = "Diana Wynne Jones",  Isbn = "978-0064473934", PublishedDate = new DateOnly(1982,  1,  1), Owner = "Lukey" },
            new Book { Id = 16, Title = "The Magicians of Caprona",    Author = "Diana Wynne Jones",  Isbn = "978-0064473927", PublishedDate = new DateOnly(1980,  1,  1), Owner = "Liam",    LoanStatus = LoanStatus.Borrowed, Borrower = "David" },
            new Book { Id = 17, Title = "Castle in the Air",           Author = "Diana Wynne Jones",  Isbn = "978-0061447259", PublishedDate = new DateOnly(1990,  1,  1), Owner = "David" },
            new Book { Id = 18, Title = "Deep Secret",                 Author = "Diana Wynne Jones",  Isbn = "978-0061020261", PublishedDate = new DateOnly(1997,  1,  1), Owner = "Bill" },
            new Book { Id = 19, Title = "A Tale of Time City",         Author = "Diana Wynne Jones",  Isbn = "978-0064407588", PublishedDate = new DateOnly(1987,  1,  1), Owner = "Dave" },
            new Book { Id = 20, Title = "Dogsbody",                    Author = "Diana Wynne Jones",  Isbn = "978-0064473880", PublishedDate = new DateOnly(1975,  1,  1), Owner = "Hana",    LoanStatus = LoanStatus.Borrowed, Borrower = "Rob" },
            // Bob Shaw
            new Book { Id = 21, Title = "Other Days, Other Eyes",      Author = "Bob Shaw",           Isbn = "978-0575016972", PublishedDate = new DateOnly(1972,  1,  1), Owner = "Rob" },
            new Book { Id = 22, Title = "Orbitsville",                 Author = "Bob Shaw",           Isbn = "978-0575018297", PublishedDate = new DateOnly(1975,  1,  1), Owner = "Jess" },
            new Book { Id = 23, Title = "The Ragged Astronauts",       Author = "Bob Shaw",           Isbn = "978-0575038233", PublishedDate = new DateOnly(1986,  1,  1), Owner = "Geordie", LoanStatus = LoanStatus.Borrowed, Borrower = "Damo" },
            new Book { Id = 24, Title = "Night Walk",                  Author = "Bob Shaw",           Isbn = "978-0575016965", PublishedDate = new DateOnly(1967,  1,  1), Owner = "Damo" },
            new Book { Id = 25, Title = "The Two-Timers",              Author = "Bob Shaw",           Isbn = "978-0575016958", PublishedDate = new DateOnly(1968,  1,  1), Owner = "Lukey" },
            new Book { Id = 26, Title = "A Wreath of Stars",           Author = "Bob Shaw",           Isbn = "978-0575021617", PublishedDate = new DateOnly(1976,  1,  1), Owner = "Liam",    LoanStatus = LoanStatus.Borrowed, Borrower = "Bill" },
            new Book { Id = 27, Title = "Who Goes Here?",              Author = "Bob Shaw",           Isbn = "978-0575024359", PublishedDate = new DateOnly(1977,  1,  1), Owner = "David" },
            new Book { Id = 28, Title = "Orbitsville Departure",       Author = "Bob Shaw",           Isbn = "978-0575033634", PublishedDate = new DateOnly(1983,  1,  1), Owner = "Bill" },
            new Book { Id = 29, Title = "The Wooden Spaceships",       Author = "Bob Shaw",           Isbn = "978-0575042100", PublishedDate = new DateOnly(1988,  1,  1), Owner = "Dave" },
            new Book { Id = 30, Title = "Vertigo",                     Author = "Bob Shaw",           Isbn = "978-0575025523", PublishedDate = new DateOnly(1978,  1,  1), Owner = "Hana" }
        );
    }
}
