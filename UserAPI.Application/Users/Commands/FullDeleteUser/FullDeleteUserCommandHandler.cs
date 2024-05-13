using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Commands.FullDeleteUser
{
	public class FullDeleteUserCommandHandler : IRequestHandler<FullDeleteUserCommand, Unit>
	{
		private readonly IUserDbContext _context;

		public FullDeleteUserCommandHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(FullDeleteUserCommand command, CancellationToken cancellationToken)
		{
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.AdminLogin && user.Password == command.AdminPassword, cancellationToken);
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.Login, cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (user == null) throw new UserNotFoundException(command.Login);
			if (!admin.Admin) throw new AccessRightsException();

			_context.Users.Remove(user);
			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
