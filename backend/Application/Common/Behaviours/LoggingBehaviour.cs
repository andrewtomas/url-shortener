using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var name = typeof(TRequest).Name;
        _logger.LogInformation("{Request} - Request Recieved.", name);
        var timer = Stopwatch.StartNew();
        var response = await next();
        timer.Stop();
        _logger.LogInformation("{Request} - Request completed in {Time} ms.", name, timer.ElapsedMilliseconds);
        return response;
    }
}