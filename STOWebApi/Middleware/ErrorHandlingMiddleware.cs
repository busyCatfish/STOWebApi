using Microsoft.AspNetCore.Http;
using STOWebApi.Business.Validation;
using STOWebApi.Data.Validation;

namespace STOWebApi.Middleware
{
	public class ErrorHandlingMiddleware : IMiddleware
	{
		private readonly ILogger<ErrorHandlingMiddleware> _logger;

		public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (STOSystemException ex)
			{
				_logger.LogError(ex, "STOSystemException occurred.");

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync(ex.Message);
			}
			catch (STODataBaseException ex)
			{
				_logger.LogError(ex, "STODataBaseException occurred.");

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception occurred.");

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong. Please try again later.");
			}
		}
	}
}
