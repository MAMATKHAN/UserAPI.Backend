
namespace UserAPI.Application.Common.Exceptions
{
	public class LoginUniquenessException : Exception
	{
		public LoginUniquenessException()
		: base("This login already exists") { }
	}
}
