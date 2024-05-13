
namespace UserAPI.Application.Common.Exceptions
{
	public class UserActivityException : Exception
	{
		public UserActivityException(string login)
		: base($"User({login}) is not active") { }
	}
}
