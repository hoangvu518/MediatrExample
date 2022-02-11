using MediatrExample.Core.Services;
using Microsoft.IO;
namespace MediatrExample.Middlewares
{
    public class RequestResponseLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggerMiddleware(RequestDelegate next, ILogger<RequestResponseLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, SecurityAuditLogService securityAuditLogService)
        {
            // Middleware is enabled only when the EnableRequestResponseLogging config value is set.
            var requestBody = await ReadBodyFromRequest(httpContext.Request);
            _logger.LogInformation($"HTTP request information:\n" +
                $"\tMethod: {httpContext.Request.Method}\n" +
                $"\tPath: {httpContext.Request.Path}\n" +
                $"\tQueryString: {httpContext.Request.QueryString}\n" +
                $"\tHeaders: {FormatHeaders(httpContext.Request.Headers)}\n" +
                $"\tSchema: {httpContext.Request.Scheme}\n" +
                $"\tHost: {httpContext.Request.Host}\n" +
                $"\tBody: {requestBody}");
            securityAuditLogService.BuildRequestLog(requestBody);

            // Temporarily replace the HttpResponseStream, which is a write-only stream, with a MemoryStream to capture it's value in-flight.
            var originalResponseBody = httpContext.Response.Body;
            using var newResponseBody = new MemoryStream();
            httpContext.Response.Body = newResponseBody;

            // Call the next middleware in the pipeline
            await _next(httpContext);

            newResponseBody.Seek(0, SeekOrigin.Begin);
            using var responseBody = new StreamReader(httpContext.Response.Body);
            var responseBodyText = await responseBody.ReadToEndAsync();
            //var responseBodyText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            _logger.LogInformation($"HTTP request information:\n" +
                $"\tStatusCode: {httpContext.Response.StatusCode}\n" +
                $"\tContentType: {httpContext.Response.ContentType}\n" +
                $"\tHeaders: {FormatHeaders(httpContext.Response.Headers)}\n" +
                $"\tBody: {responseBodyText}");
            securityAuditLogService.BuildResponseLog(responseBodyText);
            _logger.LogInformation($"Security Audit Log Service: {securityAuditLogService.ToJson()}");
            newResponseBody.Seek(0, SeekOrigin.Begin);
            await newResponseBody.CopyToAsync(originalResponseBody);
        }

        private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));

        private static async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            // Ensure the request's body can be read multiple times (for the next middlewares in the pipeline).
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            // Reset the request's body stream position for next middleware in the pipeline.
            request.Body.Position = 0;
            return requestBody;
        }

    }

}
