using Application.Exceptions;
using FluentValidation;
using System.Net;
using WebAPI.Models;
using WebAPI.Utils;
using Newtonsoft.Json;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadHttpRequestException brEx)
            {
                await HandleExceptionWriteAsync(httpContext, HttpStatusCode.BadRequest, brEx.Message);
                _logger.LogError(brEx, "{user}'s Http request does not has a valid clientFlowId", HttpContextUtil.GetCurrentUsername(httpContext));
            }
            catch (ValidationException vEx)
            {
                await HandleExceptionWriteAsync(httpContext, HttpStatusCode.BadRequest, vEx.Errors);
            }
            catch (SerializerException ex)
            {
                _logger.LogError(ex, "Response from the gateway's uri {path} was not able to be serilized - user {user}", ex.Path, HttpContextUtil.GetCurrentUsername(httpContext));
                await HandleExceptionWriteAsync(httpContext, HttpStatusCode.InternalServerError, "Serialization Error");
            }
            catch (GatewayException vEx) //Pass direct error from CISL
            {
                _logger.LogError(vEx, "{user}'s Http {method} request to the uri {path} responded with {statusCode}", HttpContextUtil.GetCurrentUsername(httpContext), vEx.Verb, vEx.Path, vEx.StatusCode);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                // httpContext.Response.StatusCode = (int)vEx.StatusCode;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    statusCode = (int)vEx.StatusCode,
                    response = vEx.Response
                }));
                // await httpContext.Response.WriteAsync(JsonConvert.SerializeObject((object)vEx.Response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request from ({user}) has failed, see the below error", HttpContextUtil.GetCurrentUsername(httpContext));
                await HandleExceptionWriteAsync(httpContext, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task HandleExceptionWriteAsync(HttpContext context, HttpStatusCode code, dynamic error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                statusCode = context.Response.StatusCode,
                response = error
            }.ToString());
        }
    }
}
