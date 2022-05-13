using Medevia.API.UI.Middleware;

namespace Medevia.API.UI.ExtensionMethods
{
    public static class MiddlewareMethods
    {
        public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogRequestMiddleware>();
            return app;
        }
    }
}
