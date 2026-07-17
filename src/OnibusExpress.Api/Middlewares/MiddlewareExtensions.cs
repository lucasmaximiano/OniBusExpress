namespace OnibusExpress.Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionMiddleware(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationExceptionMiddleware>();
        }
    }
}
