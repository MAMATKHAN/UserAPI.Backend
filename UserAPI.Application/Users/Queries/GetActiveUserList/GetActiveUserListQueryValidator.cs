using FluentValidation;

namespace UserAPI.Application.Users.Queries.GetActiveUserList
{
	public class GetActiveUserListQueryValidator : AbstractValidator<GetActiveUserListQuery>
	{
		public GetActiveUserListQueryValidator()
		{
			RuleFor(query => query.AdminLogin).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
			RuleFor(query => query.AdminPassword).NotEmpty().Matches(@"^[A-Za-z0-9]+$");
		}
	}
}
