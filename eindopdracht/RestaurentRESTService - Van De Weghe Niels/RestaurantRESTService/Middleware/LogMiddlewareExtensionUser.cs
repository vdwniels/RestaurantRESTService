namespace RestautantRESTServiceUser.Middleware
{
    public static class LogMiddlewareExtensionUser
    {
        public static IApplicationBuilder UseLogURLMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddlewareUser>();
        }

    }
}
