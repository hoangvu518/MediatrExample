using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace MediatrExample.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env)
        {
            
            try
            {
                var test = _httpContextAccessor.HttpContext;
                _logger.LogInformation("Request middleware");
                await _next(httpContext);
                _logger.LogInformation("Response middleware");
            }
            catch (Exception exception)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                int statusCode;
                switch (exception)
                {
                    case ResourceNotFoundException e:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedException e:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                response.StatusCode = statusCode;
                var result = JsonSerializer.Serialize(new ProblemDetails { Detail = exception.Message, Status = statusCode });
                await response.WriteAsync(result);
            }
        }

    }
}
