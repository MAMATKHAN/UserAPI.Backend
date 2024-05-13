using MediatR;

namespace UserAPI.Application.Users.Commands.SoftDeleteUser
{
	public class SoftDeleteUserCommand : IRequest<Unit>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
		public string Login { get; set; }
	}
}
