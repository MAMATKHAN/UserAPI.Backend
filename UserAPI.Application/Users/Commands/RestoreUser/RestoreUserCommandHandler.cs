using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Commands.RestoreUser
{
	public class RestoreUserCommandHandler : IRequestHandler<RestoreUserCommand, Unit>
	{
		private readonly IUserDbContext _context;

		public RestoreUserCommandHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(RestoreUserCommand command, CancellationToken cancellationToken)
		{
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.AdminLogin && user.Password == command.AdminPassword, cancellationToken);
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Guid == command.UserId, cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (user == null) throw new UserNotFoundException(command.UserId);
			if (!admin.Admin) throw new AccessRightsException();

			user.RevokedBy = string.Empty;
			user.RevokedOn = default(DateTime);
			user.ModifiedBy = admin.Login;
			user.ModifiedOn = DateTime.Now;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
