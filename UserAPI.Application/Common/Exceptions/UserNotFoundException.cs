
namespace UserAPI.Application.Common.Exceptions
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(object key)
		: base($"User({key}) not found") { }
	}
}
