using FluentValidation;

namespace UserAPI.Application.Users.Queries.GetUserByLogin
{
	public class GetUserByLoginQueryValidator : AbstractValidator<GetUserByLoginQuery>
	{
		public GetUserByLoginQueryValidator()
		{
			RuleFor(query => query.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(query => query.AdminPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(query => query.Login).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
		}
	}
}
