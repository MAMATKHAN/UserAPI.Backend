using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Queries.GetUserByLogin
{
	public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, UserVm>
	{
		private readonly IUserDbContext _context;
		private readonly IMapper _mapper;

		public GetUserByLoginQueryHandler(IUserDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<UserVm> Handle(GetUserByLoginQuery query, CancellationToken cancellationToken)
		{
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == query.AdminLogin && user.Password == query.AdminPassword, cancellationToken);
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == query.Login, cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (!admin.Admin) throw new AccessRightsException();
			if (user == null) throw new UserNotFoundException(query.Login);

			return _mapper.Map<UserVm>(user);
		}
	}
}
