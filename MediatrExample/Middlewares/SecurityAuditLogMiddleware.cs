//using MediatrExample.Core.Services;

//namespace MediatrExample.Middlewares
//{
//    public class SecurityAuditLogMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger _logger;

//        public SecurityAuditLogMiddleware(RequestDelegate next, ILogger<SecurityAuditLogMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env, SecurityAuditLogService securityAuditLogService)
//        {
//            await securityAuditLogService.BuildRequestLog();
//            await _next(httpContext);
//            await securityAuditLogService.BuildResponseLogAsync();
//            _logger.LogInformation(securityAuditLogService.ToJson());

//        }
//    }
//}
namespace MediatrExample.Middlewares
public class Test
