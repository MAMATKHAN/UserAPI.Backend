using MediatR;
using UserAPI.Domain;

namespace UserAPI.Application.Users.Queries.GetUserByLoginAndPassword
{
	public class GetUserByLoginAndPasswordQuery : IRequest<User>
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
