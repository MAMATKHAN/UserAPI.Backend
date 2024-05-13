using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Common.Exceptions;
using UserAPI.Application.Interfaces;
using UserAPI.Application.Users.Commands.CreateUser;
using UserAPI.Domain;

namespace UserAPI.Application.Users.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
	{
		private readonly IUserDbContext _context;

		public CreateUserCommandHandler(IUserDbContext context)
		{
			_context = context;
		}

		public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
		{
			var admin = await _context.Users
				.FirstOrDefaultAsync(user => user.Login == command.AdminLogin && user.Password == command.AdminPassword, cancellationToken);
			var loginIsExist = await _context.Users.AnyAsync(user => user.Login == command.Login, cancellationToken);

			if (admin == null) throw new IncorrectUserOrLoginException();
			if (!admin.Admin) throw new AccessRightsException();
			if (loginIsExist) throw new LoginUniquenessException();

			var entity = new User
			{
				Login = command.Login,
				Password = command.Password,
				Name = command.Name,
				Gender = command.Gender,
				BirthDay = command.BirthDay,
				Admin = command.Admin,
				CreatedOn = DateTime.Now,
				CreatedBy = command.AdminLogin,
				ModifiedOn = default(DateTime),
				ModifiedBy = string.Empty,
				RevokedOn = default(DateTime),
				RevokedBy = string.Empty,
			};

			await _context.Users.AddAsync(entity, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);

			return entity.Guid;
		}
	}
}
