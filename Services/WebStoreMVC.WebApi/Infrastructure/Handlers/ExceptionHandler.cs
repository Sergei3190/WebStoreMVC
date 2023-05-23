namespace WebStoreMVC.WebApi.Handlers.Infrastructure;

public class ExceptionHandler
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandler> _logger;

	public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception error)
		{
			HandleException(context, error);
			throw;
		}
	}

	private void HandleException(HttpContext context, Exception error)
	{
		_logger.LogError(error, "Ошибка в процессе обработки запроса к {0}", context.Request.Path);
	}
}