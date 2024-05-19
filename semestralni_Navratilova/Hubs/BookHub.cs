using Microsoft.AspNetCore.SignalR;
using semestralni_Navratilova.Model;

namespace semestralni_Navratilova.Hubs
{
    public class BookHub : Hub
    {
        public async Task RefreshBooks(List<Book> books)
        {
            await Clients.All.SendAsync("RefreshBooks", books);
        }
    }
}
