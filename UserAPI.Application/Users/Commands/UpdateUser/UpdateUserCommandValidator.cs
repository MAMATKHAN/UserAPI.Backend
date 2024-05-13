using FluentValidation;

namespace UserAPI.Application.Users.Commands.UpdateUser
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(updateUserCommand => updateUserCommand.SenderLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserCommand => updateUserCommand.SenderPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserCommand => updateUserCommand.Name).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserCommand => updateUserCommand.Gender).NotNull().Must(g => g >= 0 && g <= 2);
			RuleFor(updateUserCommand => updateUserCommand.UserId).NotEmpty();
		}
	}
}
