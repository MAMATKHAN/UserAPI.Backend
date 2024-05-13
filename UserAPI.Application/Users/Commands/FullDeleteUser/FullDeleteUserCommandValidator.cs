using FluentValidation;

namespace UserAPI.Application.Users.Commands.FullDeleteUser
{
	public class FullDeleteUserCommandValidator : AbstractValidator<FullDeleteUserCommand>
	{
		public FullDeleteUserCommandValidator()
		{
			RuleFor(fullDeleteUserCommand => fullDeleteUserCommand.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(fullDeleteUserCommand => fullDeleteUserCommand.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(fullDeleteUserCommand => fullDeleteUserCommand.Login).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
		}
	}
}
