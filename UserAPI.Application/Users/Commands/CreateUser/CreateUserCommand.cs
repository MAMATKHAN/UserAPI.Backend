using MediatR;

namespace UserAPI.Application.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<Guid>
	{
		public string AdminLogin { get; set; }
		public string AdminPassword { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public int Gender { get; set; }
		public DateTime? BirthDay { get; set; }
		public bool Admin { get; set; }
	}
}
