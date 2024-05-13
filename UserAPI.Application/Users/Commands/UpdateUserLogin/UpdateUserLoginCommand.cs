using MediatR;

namespace UserAPI.Application.Users.Commands.UpdateUserLogin
{
	public class UpdateUserLoginCommand : IRequest<Unit>
	{
		public string SenderLogin { get; set; }
		public string SenderPassword { get; set; }
		public Guid UserId { get; set; }
		public string Login { get; set; }
	}
}
