namespace RestautantRESTServiceUser.Middleware
{
    public class LogMiddlewareUser
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddlewareUser(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogMiddlewareUser>();
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {

                _logger.LogInformation("Request {method} {url} => {statuscode}; optional input => {OptionalInput} ",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode,
                    context.Request?.Query);

            }
        }

    }
}
