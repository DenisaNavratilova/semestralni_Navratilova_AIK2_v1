using semestralni_Navratilova.Data;
using semestralni_Navratilova.Model;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using semestralni_Navratilova.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace semestralni_Navratilova.LibraryUsers
{
    public class LibraryUsers
    {
        private readonly IHubContext<UserHub> _context;
        private readonly LibraryContext dbContext;
        private readonly SqlTableDependency<User> _dependency;
        private readonly string _connectionString;

        public LibraryUsers(IConfiguration configuration, IHubContext<UserHub> context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DbConnectionString");
            dbContext = new LibraryContext(configuration);
            _dependency = new SqlTableDependency<User>(_connectionString, "Users");
            _dependency.OnChanged += Changed;
            _dependency.Start();
        }

        private async void Changed(object sender, RecordChangedEventArgs<User> e)
        {
            var users = await GetAllUsers();
            await _context.Clients.All.SendAsync("RefreshUsers", users);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }
    }
}
