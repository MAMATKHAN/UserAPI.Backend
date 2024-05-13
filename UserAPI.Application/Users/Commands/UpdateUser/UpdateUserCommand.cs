using MediatR;

namespace UserAPI.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommand : IRequest<Unit>
	{
		public string SenderLogin { get; set; }
		public string SenderPassword { get; set; }
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public int Gender { get; set; }
		public DateTime? BirthDay { get; set; }
	}
}
