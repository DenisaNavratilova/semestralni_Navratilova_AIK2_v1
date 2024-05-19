using semestralni_Navratilova.Data;
using semestralni_Navratilova.Model;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using semestralni_Navratilova.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace semestralni_Navratilova.LibraryBooks
{
    public class LibraryBooks
    {
        private readonly IHubContext<BookHub> _context;
        private readonly LibraryContext dbContext;
        private readonly SqlTableDependency<Book> _dependency;
        private readonly string _connectionString;

        public LibraryBooks(IConfiguration configuration, IHubContext<BookHub> context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DbConnectionString");
            dbContext = new LibraryContext(configuration);
            _dependency = new SqlTableDependency<Book>(_connectionString, "Books");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Book> e)
        {
            var books = await GetAllBooks();
            await _context.Clients.All.SendAsync("RefreshBooks", books);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await dbContext.Books.AsNoTracking().ToListAsync();
        }
    }
}
