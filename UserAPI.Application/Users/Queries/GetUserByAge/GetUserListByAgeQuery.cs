using MediatR;
using UserAPI.Application.Users.Queries.GetActiveUserList;

namespace UserAPI.Application.Users.Queries.GetUserByAge
{
	public class GetUserListByAgeQuery : IRequest<UserListVm>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
		public int Age { get; set; }
	}
}
