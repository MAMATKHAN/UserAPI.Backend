using MediatR;

namespace UserAPI.Application.Users.Queries.GetActiveUserList
{
	public class GetActiveUserListQuery : IRequest<UserListVm>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
	}
}
