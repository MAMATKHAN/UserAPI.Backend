using MediatR;

namespace UserAPI.Application.Users.Commands.FullDeleteUser
{
	public class FullDeleteUserCommand : IRequest<Unit>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
		public string Login { get; set; }
	}
}
