
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediatrExample.Core.Services
{
    public class SecurityAuditLogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private SALRecord _salRecord = new();
        private readonly AppDb _db;
        public SecurityAuditLogService(IHttpContextAccessor httpContextAccessor, ILogger<SecurityAuditLogService> logger, AppDb db)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger; ;
            _db = db;
        }

        public void BuildRequestLog(string requestBody)
        {
            var httRequest = _httpContextAccessor.HttpContext.Request;
            var httpMethod = httRequest.Method;
            var httpRequestPath = httRequest.Path;
            _salRecord.HttpMethod = httpMethod;
            _salRecord.RequestPath = httpRequestPath;
            _salRecord.RequestBody = requestBody;
        }
        public void BuildResponseLog(string responseBody)
        {
            var httpResponse = _httpContextAccessor.HttpContext.Response;
            var httpStatusCode = httpResponse.StatusCode;
            _salRecord.ResponseBody = responseBody;
            _salRecord.HttpStatusCode = httpStatusCode;
            _logger.LogInformation(responseBody);
            _logger.LogInformation("{@_salRecord}", _salRecord);
            _logger.LogInformation(_salRecord.ResponseBody);
            var newStudent = new Core.Domain.Student(ToJson(), responseBody);
             _db.Student.Add(newStudent);
             _db.SaveChanges();
        }
        public string ToJson()
        {
            return JsonSerializer.Serialize(_salRecord);
        }

    }

    public class SALRecord
    {
        public string HttpMethod { get; set; }
        public string RequestPath { get; set; }
        public string RequestBody { get; set; }
        [JsonSerializable(ty)]
        public string ResponseBody { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
