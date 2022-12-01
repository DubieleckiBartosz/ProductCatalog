using Polly;
using System.Data;
using System.Data.SqlClient;
using ProductCatalog.Application.Logging;

namespace ProductCatalog.Infrastructure.Connections;

public class DatabaseConnection<TRepository>
{
    private readonly string _connectionString;
    private readonly ILoggerManager<TRepository> _loggerManager;
    private readonly AsyncPolicy _retryAsyncPolicyQuery;
    private readonly AsyncPolicy _retryAsyncPolicyConnection;
    public DatabaseConnection(string connectionString, ILoggerManager<TRepository> loggerManager)
    {
        _connectionString = connectionString;
        _loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));

        var policy = new PolicySetup();
        _retryAsyncPolicyConnection = policy.PolicyConnectionAsync(_loggerManager);
        _retryAsyncPolicyQuery = policy.PolicyQueryAsync(_loggerManager);
    }

    public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> funcData)
    {
        try
        {
            SqlConnection? connection;
            await using (connection = new SqlConnection(_connectionString))
            {
                await _retryAsyncPolicyConnection.ExecuteAsync(async () => await connection.OpenAsync());

                _loggerManager.LogInformation(null, message: "Connection has been opened");

                return await _retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
            }
        }
        catch (TimeoutException ex)
        {
            throw new Exception($"Timeout sql exception: {ex}");
        }
        catch (SqlException ex)
        {
            throw new Exception($"Sql exception: {ex}");
        }
    }
}