using MediatR;

namespace UserAPI.Application.Users.Queries.GetUserByLogin
{
	public class GetUserByLoginQuery : IRequest<UserVm>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
		public string Login { get; set; }
	}
}
