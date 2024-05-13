
namespace UserAPI.Application.Common.Exceptions
{
	public class AccessRightsException : Exception
	{
		public AccessRightsException() 
		: base("Insufficient access rights") { }
	}
}
