using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Queries.GetActiveUserList
{
	public class GetActiveUserListQueryHandler : IRequestHandler<GetActiveUserListQuery, UserListVm>
	{
		private readonly IUserDbContext _context;

		public GetActiveUserListQueryHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<UserListVm> Handle(GetActiveUserListQuery query, CancellationToken cancellationToken)
		{
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == query.AdminLogin && user.Password == query.AdminPassword, cancellationToken);

			var users = await _context.Users
				.Where(user => string.IsNullOrEmpty(user.RevokedBy))
				.OrderBy(user => user.CreatedOn)
				.ToListAsync(cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (!admin.Admin) throw new AccessRightsException();

			return new UserListVm { Users = users };
		}
	}
}
