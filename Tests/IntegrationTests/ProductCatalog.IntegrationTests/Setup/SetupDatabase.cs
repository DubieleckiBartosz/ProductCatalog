using System.Data;
using System.Data.SqlClient;
using Dapper;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.IntegrationTests.Setup;

public class SetupDatabase
{
    private readonly string? _connection;

    public SetupDatabase(string connectionString)
    {
        _connection = connectionString;
    }

    public async Task<bool> SetFakeProducts(List<Product> products)
    {
        await using var connection = new SqlConnection(_connection);
        await connection.OpenAsync();

        DataTable tokenTable = new DataTable();
        tokenTable.Columns.Add(new DataColumn("Name", typeof(string)));
        tokenTable.Columns.Add(new DataColumn("Price", typeof(decimal)));
        tokenTable.Columns.Add(new DataColumn("Code", typeof(string))); 

        foreach (var product in products)
        {
            tokenTable.Rows.Add(product.Name, product.Price, product.Code);
        } 

        var param = new DynamicParameters(); 
        param.Add("@tvp", 
            tokenTable.AsTableValuedParameter("ProductsTableType"));
        var result =
            await connection.ExecuteAsync("product_createProducts_I", param, commandType: CommandType.StoredProcedure);

        return result > 0;
    }
}