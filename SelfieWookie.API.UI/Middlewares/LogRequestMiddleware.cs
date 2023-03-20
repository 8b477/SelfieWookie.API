namespace SelfieWookie.API.UI.Middlewares
{
    public class LogRequestMiddleware
    {
        private RequestDelegate? _next;
        private ILogger<LogRequestMiddleware>? _logger;

        public LogRequestMiddleware(RequestDelegate? next, ILogger<LogRequestMiddleware>? logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_logger is not null)
            {
                _logger.LogDebug(context.Request.Path.Value);
            }

            if (_next is not null)
            {
               await _next(context);
            }
        }
    }
}
