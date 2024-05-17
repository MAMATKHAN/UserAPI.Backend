using MediatR;

namespace UserAPI.Application.Users.Commands.RestoreUser
{
	public class RestoreUserCommand : IRequest<Unit>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
		public string Login { get; set; }
	}
}
