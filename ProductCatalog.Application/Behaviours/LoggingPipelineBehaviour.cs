using MediatR.Pipeline;
using ProductCatalog.Application.Logging;

namespace ProductCatalog.Application.Behaviours;

public class LoggingPipelineBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILoggerManager<LoggingPipelineBehaviour<TRequest>> _logger;

    public LoggingPipelineBehaviour(ILoggerManager<LoggingPipelineBehaviour<TRequest>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;
        _logger.LogInformation(null, $"EventManagement Request: {name} - {request}");

        return Task.CompletedTask;
    }
}