namespace CustomMiddlewareExample.CustomMiddleware
{
    public static class LoggerExtension
    {
        public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomLoggerMiddleWare>();
        }

    }
}
