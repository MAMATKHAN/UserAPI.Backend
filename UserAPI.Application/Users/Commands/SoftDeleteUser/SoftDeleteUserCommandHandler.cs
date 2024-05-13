using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Commands.SoftDeleteUser
{
	public class SoftDeleteUserCommandHandler : IRequestHandler<SoftDeleteUserCommand, Unit>
	{
		private readonly IUserDbContext _context;

		public SoftDeleteUserCommandHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(SoftDeleteUserCommand command, CancellationToken cancellationToken)
		{
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.AdminLogin && user.Password == command.AdminPassword, cancellationToken);
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.Login, cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (user == null) throw new UserNotFoundException(command.Login);
			if (!admin.Admin) throw new AccessRightsException();

			user.RevokedOn = DateTime.Now;
			user.RevokedBy = admin.Login;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
