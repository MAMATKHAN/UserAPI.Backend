using FluentValidation;

namespace UserAPI.Application.Users.Commands.CreateUser
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(createUserCommand => createUserCommand.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(createUserCommand => createUserCommand.AdminPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(createUserCommand => createUserCommand.Login).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(createUserCommand => createUserCommand.Password).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(createUserCommand => createUserCommand.Name).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(createUserCommand => createUserCommand.Gender).NotNull().Must(g => g >= 0 && g <= 2);
		}
	}
}
