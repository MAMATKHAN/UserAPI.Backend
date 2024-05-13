using FluentValidation;

namespace UserAPI.Application.Users.Commands.UpdateUserLogin
{
	public class UpdateUserLoginCommandValidator : AbstractValidator<UpdateUserLoginCommand>
	{
		public UpdateUserLoginCommandValidator()
		{
			RuleFor(updateUserLoginCommand => updateUserLoginCommand.SenderLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserLoginCommand => updateUserLoginCommand.SenderPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserLoginCommand => updateUserLoginCommand.Login).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(updateUserLoginCommand => updateUserLoginCommand.UserId).NotEmpty();
		}
	}
}
