using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackFinances.DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _configuration;

    public SqlDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure,
                                                    string connectionId = "Default")
    {
        using var connection = await CreateConnectionAsync(connectionId);
        return await connection.QueryAsync<T>(storedProcedure,
                                              commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> QueryAsync<T, U>(string storedProcedure,
                                                       U parameters,
                                                       string connectionId = "Default")
    {
        using var connection = await CreateConnectionAsync(connectionId);
        return await connection.QueryAsync<T>(storedProcedure,
                                              parameters,
                                              commandType: CommandType.StoredProcedure);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string storedProcedure,
                                                     string connectionId = "Default")
    {
        using var connection = await CreateConnectionAsync(connectionId);
        return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure,
                                                            commandType: CommandType.StoredProcedure);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T, U>(string storedProcedure,
                                                        U parameters,
                                                        string connectionId = "Default")
    {
        using var connection = await CreateConnectionAsync(connectionId);
        return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure,
                                                            parameters,
                                                            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> ExecuteAsync<T>(string storedProcedure,
                                            T parameters,
                                            string connectionId = "Default")
    {
        using var connection = await CreateConnectionAsync(connectionId);
        var rowsAffected = await connection.ExecuteAsync(storedProcedure,
                                                         parameters,
                                                         commandType: CommandType.StoredProcedure);
        return rowsAffected > 0;
    }

    private async Task<IDbConnection> CreateConnectionAsync(string connectionId)
    {
        var connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        await connection.OpenAsync();
        return connection;
    }
}
