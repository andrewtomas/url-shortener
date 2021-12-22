using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : CQRSResponse, new()
{
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IServiceProvider serviceProvider;

    public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        this.serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        // If there is a validator configured, get the validator and confirm the request is valid.
        var validator = serviceProvider.GetService<IValidator<TRequest>>();
        if (validator != null)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("{Request} - Validator configured. Running validation.", requestName);
            var result = await validator.Validate(request);
            if (result.IsSuccess == false)
            {
                _logger.LogWarning("{Request} - Validation failed. Reason: {Reason}", requestName, result.ErrorMessage);
                var response = new TResponse();
                response.BadRequest(result);
                return response;
            }

            _logger.LogInformation("{Request} - Validation successful.", requestName);
        }

        // Go to the next behaviour in the pipeline
        return await next();
    }
}