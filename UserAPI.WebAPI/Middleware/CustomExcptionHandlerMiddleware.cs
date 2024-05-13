using System.Net;
using System.Text.Json;
using UserAPI.Application.Common.Exceptions;
using FluentValidation;

namespace UserAPI.WebAPI.Middleware
{
	public class CustomExcptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomExcptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception exception)
			{
				await HandleExceptionAsync(context, exception);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError;
			var result = string.Empty;
			switch (exception)
			{
				case ValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result = JsonSerializer.Serialize(validationException.Errors);
					break;
				case UserNotFoundException userNotFoundException:
					code = HttpStatusCode.NotFound;
					break;
				case IncorrectUserOrLoginException incorrectUserOrLoginException:
					code = HttpStatusCode.Unauthorized;
					break;
				case AccessRightsException accessRightsException:
					code = HttpStatusCode.Forbidden;
					break;
				case LoginUniquenessException loginUniquenessException:
					code = HttpStatusCode.Conflict;
					break;
				case UserActivityException userActivityException:
					code = HttpStatusCode.Forbidden;
					break;
			}

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;

			if (result == string.Empty)
			{
				result = JsonSerializer.Serialize(new { error = exception.Message });
			}

			await context.Response.WriteAsync(result);
		}
	}
}
