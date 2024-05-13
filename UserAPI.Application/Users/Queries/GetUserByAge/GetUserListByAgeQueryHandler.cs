using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;
using UserAPI.Application.Users.Queries.GetActiveUserList;

namespace UserAPI.Application.Users.Queries.GetUserByAge
{
	public class GetUserListByAgeQueryHandler : IRequestHandler<GetUserListByAgeQuery, UserListVm>
	{
		private readonly IUserDbContext _context;

		public GetUserListByAgeQueryHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<UserListVm> Handle(GetUserListByAgeQuery query, CancellationToken cancellationToken)
		{
			var now = DateTime.Today;
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == query.AdminLogin && user.Password == query.AdminPassword, cancellationToken);
			var users = await _context.Users
				.Where(user => (user.BirthDay != null) &&
						(now.Year - user.BirthDay.Value.Year - 1 +
						(((now.Month > user.BirthDay.Value.Month) || 
						(now.Month == user.BirthDay.Value.Month) && 
						(now.Day >= user.BirthDay.Value.Day)) ? 1 : 0) > query.Age))
				.ToListAsync(cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (!admin.Admin) throw new AccessRightsException();

			return new UserListVm { Users = users };
		}
	}
}
