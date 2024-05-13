using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Commands.UpdateUserLogin
{
	public class UpdateUserLoginCommandHandler : IRequestHandler<UpdateUserLoginCommand, Unit>
	{
		private readonly IUserDbContext _context;

		public UpdateUserLoginCommandHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(UpdateUserLoginCommand command, CancellationToken cancellationToken)
		{
			var userMaker = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.SenderLogin && user.Password == command.SenderPassword, cancellationToken);
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Guid == command.UserId, cancellationToken);
			var loginIsExist = await _context.Users.AnyAsync(user => user.Login == command.Login, cancellationToken);

			if (userMaker == null) throw new IncorrectUserOrLoginException();
			if (user == null) throw new UserNotFoundException(command.UserId);
			if ((!userMaker.Admin) && (userMaker.Login != user.Login)) throw new AccessRightsException();
			if (!string.IsNullOrEmpty(user.RevokedBy)) throw new UserActivityException(user.Login);
			if (loginIsExist) throw new LoginUniquenessException();

			user.Login = command.Login;
			user.ModifiedBy = command.SenderLogin;
			user.ModifiedOn = DateTime.Now;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
