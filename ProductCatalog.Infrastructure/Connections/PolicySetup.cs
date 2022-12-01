using System.Data.SqlClient;
using Polly;
using ProductCatalog.Application.Logging;


namespace ProductCatalog.Infrastructure.Connections
{
    public class PolicySetup
    {
        private const int RetryCount = 3;
        public PolicySetup()
        {
        }
        public AsyncPolicy PolicyConnectionAsync<T>(ILoggerManager<T> logger) => Policy
            .Handle<SqlException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(
                RetryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    logger?.LogError(new
                    {
                        RetryAttempt = retryCount,
                        ExceptionMessage = exception?.Message,
                        Waiting = timeSpan.Seconds
                    });
                }
            );


        public AsyncPolicy PolicyQueryAsync<T>(ILoggerManager<T> logger) => Policy.Handle<SqlException>()
            .WaitAndRetryAsync(
                RetryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    logger?.LogError(new
                    {
                        RetryAttempt = retryCount,
                        ExceptionMessage = exception?.Message,
                        ProcedureName = exception == null ? "Exception null" : this.GetProcedure(exception),
                        Waiting = timeSpan.Seconds
                    });
                }
            );


        #region Private

        private string GetProcedure(Exception exception) => exception is SqlException ex ? ex.Procedure : string.Empty;

        #endregion
    }
}
