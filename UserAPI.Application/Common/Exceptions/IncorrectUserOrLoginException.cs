
namespace UserAPI.Application.Common.Exceptions
{
	public class IncorrectUserOrLoginException : Exception
	{
		public IncorrectUserOrLoginException()
		: base("Incorrect login or password") { }
	}
}
