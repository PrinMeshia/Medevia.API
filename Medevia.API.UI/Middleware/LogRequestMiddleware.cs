namespace Medevia.API.UI.Middleware
{
    public class LogRequestMiddleware
    {
        #region Fields
        private RequestDelegate _next;
        private ILogger<LogRequestMiddleware> _logger;


        #endregion

        #region Constructors
        public LogRequestMiddleware(RequestDelegate next, ILogger<LogRequestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion
        #region Public methods
        public async Task Invoke(HttpContext context)
        {
            _logger.LogDebug(context.Request.Path.Value);
            await _next(context);
        }
        #endregion
    }
}
