using FluentValidation;

namespace UserAPI.Application.Users.Commands.UpdateUserPassword
{
	public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
	{
		public UpdateUserPasswordCommandValidator()
		{
			RuleFor(updateUserPasswordCommand => updateUserPasswordCommand.SenderLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserPasswordCommand => updateUserPasswordCommand.SenderPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserPasswordCommand => updateUserPasswordCommand.Password).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserPasswordCommand => updateUserPasswordCommand.UserId).NotEmpty();
		}
	}
}
