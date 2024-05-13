namespace UserAPI.WebAPI.Middleware
{
	public static class CustomExcptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExcptionHandlerMiddleware>();
		}
	}
}
