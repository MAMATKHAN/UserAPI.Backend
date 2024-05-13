using UserAPI.Domain;

namespace UserAPI.Application.Users.Queries.GetActiveUserList
{
	public class UserListVm
	{
		public IList<User> Users { get; set; }
	}
}
