using semestralni_Navratilova.Data;
using semestralni_Navratilova.Model;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using semestralni_Navratilova.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace semestralni_Navratilova.LibraryBorrowings
{
    public class LibraryBorrowings
    {
        private readonly IHubContext<BorrowingHub> _context;
        private readonly LibraryContext dbContext;
        private readonly SqlTableDependency<Borrowing> _dependency;
        private readonly string _connectionString;

        public LibraryBorrowings(IConfiguration configuration, IHubContext<BorrowingHub> context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DbConnectionString");
            dbContext = new LibraryContext(configuration);
            _dependency = new SqlTableDependency<Borrowing>(_connectionString, "Borrowings");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<Borrowing> e)
        {
            var borrowings = await GetAllBorrowings();
            await _context.Clients.All.SendAsync("RefreshBorrowings", borrowings);
        }

        public async Task<List<Borrowing>> GetAllBorrowings()
        {
            return await dbContext.Borrowings.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetUserId(int borrowingId)
        {
            var borrowing = await dbContext.Borrowings.FirstOrDefaultAsync(b => b.BorrowingId == borrowingId);
            if (borrowing != null)
            {
                return borrowing.UserId;
            }
            else
            {
                throw new Exception("Zápůjčka s tímto ID nebyla nalezena.");
            }
        }
    }
}
