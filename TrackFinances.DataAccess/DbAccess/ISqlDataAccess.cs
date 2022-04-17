
namespace TrackFinances.DataAccess.DbAccess;

public interface ISqlDataAccess
{
    Task<bool> ExecuteAsync<T>(string storedProcedure,
                               T parameters,
                               string connectionId = "Default");
    Task<IEnumerable<T>> QueryAsync<T, U>(string storedProcedure,
                                          U parameters,
                                          string connectionId = "Default");
    Task<T> QueryFirstOrDefaultAsync<T, U>(string storedProcedure,
                                           U parameters,
                                           string connectionId = "Default");
}