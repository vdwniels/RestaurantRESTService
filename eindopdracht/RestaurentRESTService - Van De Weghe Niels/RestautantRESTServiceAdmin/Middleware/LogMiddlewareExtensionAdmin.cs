namespace RestautantRESTServiceAdmin.Middleware
{
    public static class LogMiddlewareExtensionAdmin
    {
        public static IApplicationBuilder UseLogURLMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddlewareAdmin>();
        }

    }
}
