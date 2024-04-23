using Serilog.Events;
using System.Diagnostics;
using System.Net;
using WebAPI.Utils;

namespace WebAPI.Middlewares
{
    public class RequestsLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public RequestsLoggerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            _logger.LogInformation("HTTP {RequestMethod} {RequestPath} ({name})", context.Request.Method, context.Request.Path, HttpContextUtil.GetCurrentUsername(context));
            await _next(context);
            watch.Stop();
            _logger.LogInformation("HTTP {RequestMethod} {RequestPath} ({name}) responded {StatusCode} in {Elapsed:0.0000}ms", context.Request.Method, context.Request.Path, HttpContextUtil.GetCurrentUsername(context), context.Response.StatusCode, watch.ElapsedMilliseconds);

        }
    }
}
