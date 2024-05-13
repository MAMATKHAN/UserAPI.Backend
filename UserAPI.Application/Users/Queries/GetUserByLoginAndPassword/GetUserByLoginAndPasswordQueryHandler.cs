using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;
using UserAPI.Domain;

namespace UserAPI.Application.Users.Queries.GetUserByLoginAndPassword
{
	public class GetUserByLoginAndPasswordQueryHandler : IRequestHandler<GetUserByLoginAndPasswordQuery, User>
	{
		private readonly IUserDbContext _context;

		public GetUserByLoginAndPasswordQueryHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<User> Handle(GetUserByLoginAndPasswordQuery query, CancellationToken cancellationToken)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == query.Login && user.Password == query.Password, cancellationToken);

			if (user == null) throw new IncorrectUserOrLoginException();
			if (!string.IsNullOrEmpty(user.RevokedBy)) throw new UserActivityException(query.Login);

			return user;
		}
	}
}
