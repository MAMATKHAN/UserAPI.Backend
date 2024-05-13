using MediatR;

namespace UserAPI.Application.Users.Commands.UpdateUserPassword
{
	public class UpdateUserPasswordCommand : IRequest<Unit>
	{
		public string SenderLogin { get; set; }
		public string SenderPassword { get; set; }
		public Guid UserId { get; set; }
		public string Password { get; set; }
	}
}
