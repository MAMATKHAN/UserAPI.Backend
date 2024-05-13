using FluentValidation;

namespace UserAPI.Application.Users.Commands.SoftDeleteUser
{
	public class SoftDeleteUserCommandValidator : AbstractValidator<SoftDeleteUserCommand>
	{
		public SoftDeleteUserCommandValidator()
		{
			RuleFor(softDeleteUserCommand => softDeleteUserCommand.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(softDeleteUserCommand => softDeleteUserCommand.AdminPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(softDeleteUserCommand => softDeleteUserCommand.Login).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
		}
	}
}
