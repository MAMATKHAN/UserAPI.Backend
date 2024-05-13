using FluentValidation;

namespace UserAPI.Application.Users.Queries.GetUserByLoginAndPassword
{
	public class GetUserByLoginAndPasswordQueryValidator : AbstractValidator<GetUserByLoginAndPasswordQuery>
	{
		public GetUserByLoginAndPasswordQueryValidator()
		{
			RuleFor(query => query.Login).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(query => query.Password).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
		}
	}
}
