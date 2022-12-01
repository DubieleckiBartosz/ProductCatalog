using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using ProductCatalog.Application.Logging;
using ProductCatalog.Application.Options;
using ProductCatalog.Infrastructure.Connections;

namespace ProductCatalog.Infrastructure.Repositories;

public abstract class BaseRepository<TRepository>
{
    private readonly DatabaseConnection<TRepository> _connection;

    protected BaseRepository(IOptions<ConnectionStrings> connectionString, ILoggerManager<TRepository> loggerManager)
    {
        var connectionStrings =
            connectionString?.Value ?? throw new ArgumentNullException(nameof(connectionString));

        _connection = new DatabaseConnection<TRepository>(connectionStrings.DefaultConnection, loggerManager);
    }
    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType? commandType = null)
    {
        return await this._connection.WithConnection(
            async _ => await _.QueryAsync<T>(sql: sql, param: param,
                commandType: commandType));
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType? commandType = null)
    {
        return await this._connection.WithConnection(
            async _ => await _.ExecuteAsync(sql: sql, param: param,
                commandType: commandType));
    }
}