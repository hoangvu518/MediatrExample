using System.Reflection;

namespace MediatrExample.Features.Shared
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Request
            _logger.LogInformation($"Request: {typeof(TRequest).Name}");
            Type requestType = request.GetType();
            IList<PropertyInfo> requestProps = new List<PropertyInfo>(requestType.GetProperties());
            foreach (PropertyInfo prop in requestProps)
            {
                object propValue = prop.GetValue(request, null);
                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }
            var response = await next();
            //Response
            Type responseType = response.GetType();
            IList<PropertyInfo> responseProps = new List<PropertyInfo>(responseType.GetProperties());
            foreach (PropertyInfo prop in responseProps)
            {
                object propValue = prop.GetValue(response, null);
                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }
            _logger.LogInformation($"Response {typeof(TResponse).Name}");
            return response;
        }
    }
    //public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //{
    //    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
    //    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    //    {
    //        _logger = logger;
    //    }
    //    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        //Request
    //        _logger.LogInformation($"Handling {typeof(TRequest).Name}");
    //        Type myType = request.GetType();
    //        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
    //        foreach (PropertyInfo prop in props)
    //        {
    //            object propValue = prop.GetValue(request, null);
    //            _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
    //        }
    //        var response = await next();
    //        //Response
    //        _logger.LogInformation($"Handled {typeof(TResponse).Name}");
    //        return response;
    //    }
    //}
    //public class AuthBehavior<TRequest, TResponse>
    //: IPipelineBehavior<TRequest, TResponse>
    //{
    //    public Task<TResponse> Handle(TRequest request,
    //        CancellationToken cancellationToken,
    //        RequestHandlerDelegate<TResponse> next)
    //    {
    //        var user = (IPrincipal)HttpContext.Items["CurrentUser"];

    //        if (!user.Principal.IsAuthenticated)
    //            return Task.FromResult<TResponse>(default);

    //        return next;
    //    }
    //}
}
