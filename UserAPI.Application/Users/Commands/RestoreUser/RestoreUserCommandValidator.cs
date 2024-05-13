using FluentValidation;

namespace UserAPI.Application.Users.Commands.RestoreUser
{
	public class RestoreUserCommandValidator : AbstractValidator<RestoreUserCommand>
	{
		public RestoreUserCommandValidator()
		{
			RuleFor(restoreUserCommand => restoreUserCommand.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(restoreUserCommand => restoreUserCommand.AdminPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(restoreUserCommand => restoreUserCommand.UserId).NotEmpty();
		}
	}
}
