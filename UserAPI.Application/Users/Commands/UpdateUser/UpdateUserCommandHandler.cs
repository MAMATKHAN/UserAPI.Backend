using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;

namespace UserAPI.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
	{
		private readonly IUserDbContext _context;

		public UpdateUserCommandHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<Unit> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
		{
			var userMaker = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.SenderLogin && user.Password == command.SenderPassword, cancellationToken);
			var user = await _context.Users
				.FirstOrDefaultAsync(user => user.Guid == command.UserId, cancellationToken);

			if (userMaker == null) throw new IncorrectUserOrLoginException();
			if (user == null) throw new UserNotFoundException(command.UserId);
			if ((!userMaker.Admin) && (userMaker.Login != user.Login)) throw new AccessRightsException();
			if (!string.IsNullOrEmpty(userMaker.RevokedBy) && (!userMaker.Admin)) throw new UserActivityException(userMaker.Login);

			user.Name = command.Name;
			user.BirthDay = command.BirthDay;
			user.Gender = command.Gender;
			user.ModifiedBy = command.SenderLogin;
			user.ModifiedOn = DateTime.Now;

			await _context.SaveChangesAsync(cancellationToken);

			return Unit.Value;
		}
	}
}
