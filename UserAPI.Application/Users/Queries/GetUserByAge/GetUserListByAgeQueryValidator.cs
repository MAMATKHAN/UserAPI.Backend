using FluentValidation;

namespace UserAPI.Application.Users.Queries.GetUserByAge
{
	public class GetUserListByAgeQueryValidator : AbstractValidator<GetUserListByAgeQuery>
	{
		public GetUserListByAgeQueryValidator()
		{
			RuleFor(query => query.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(query => query.AdminPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(query => query.Age).NotEmpty();
		}
	}
}
