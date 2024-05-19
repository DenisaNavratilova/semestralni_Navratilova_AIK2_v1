using Microsoft.EntityFrameworkCore;
using semestralni_Navratilova.Model;

namespace semestralni_Navratilova.Data
{
    public class LibraryContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public LibraryContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Users = Set<User>();
            Books = Set<Book>();
            Borrowings = Set<Borrowing>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString"));
            options.EnableSensitiveDataLogging();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrowings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Borrowing>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.Borrowings)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Denisa",
                    LastName = "Navrátilová",
                    Birthday = "13.12.1998",
                    Adresse = "Ledecká 35, Plzeň",
                    PhoneNumber = "739300049",
                    Email = "deniska.navratilova13@gmail.com",
                    DateOfRegistration = "17.5.2024"
                });

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    ISBN = "978-80-87128-02-2",
                    Name = "Lakomec",
                    Author = "Molière",
                    Binding = "měkká / brožovaná",
                    Genre = "Literatura světová, Divadelní hry",
                    Pages = 88,
                    Publisher = "Artur",
                    Language = "český",
                    Translator = "Vladimír Mikeš",
                    Year = 2008,
                    Quantity = 5,
                    AvailablePieces = 5
                },
                new Book
                {
                    BookId = 2,
                    ISBN = "978-80-7335-647-7",
                    Name = "1984",
                    Author = "George Orwell",
                    Binding = "pevná / vázaná",
                    Genre = "Literatura světová, Romány, Sci-fi",
                    Pages = 344,
                    Publisher = "Leda",
                    Language = "český",
                    Translator = "Jan Kalandra",
                    Year = 2021,
                    Quantity = 3,
                    AvailablePieces = 3
                },
                new Book
                {
                    BookId = 3,
                    ISBN = "978-80-00-03424-9",
                    Name = "Malý princ",
                    Author = "Antoine De Saint-Exupéry",
                    Binding = "měkká / brožovaná",
                    Genre = "Literatura světová, Pro děti a mládež, Sci-fi",
                    Pages = 96,
                    Publisher = "Albatros (ČR)",
                    Language = "český",
                    Translator = "Richard Podaný",
                    Year = 2014,
                    Quantity = 0,
                    AvailablePieces = 0
                });
        }
    }
}
