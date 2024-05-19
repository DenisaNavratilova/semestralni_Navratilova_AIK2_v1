using Microsoft.AspNetCore.SignalR;
using semestralni_Navratilova.Model;

namespace semestralni_Navratilova.Hubs
{
    public class BorrowingHub : Hub
    {
        public async Task RefreshBorrowings(List<Borrowing> borrowings)
        {
            await Clients.All.SendAsync("RefreshBorrowings", borrowings);
        }
    }
}
