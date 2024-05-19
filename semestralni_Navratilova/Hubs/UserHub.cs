using Microsoft.AspNetCore.SignalR;
using semestralni_Navratilova.Model;

namespace semestralni_Navratilova.Hubs
{
	public class UserHub : Hub
	{
		public async Task RefreshUsers(List<User> users)
		{
			await Clients.All.SendAsync("RefreshUsers", users);
		}
	}
}
