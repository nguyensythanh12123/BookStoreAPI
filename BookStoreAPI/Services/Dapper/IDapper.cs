using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace BookStoreAPI.Services.Dapper
{
    public interface IDapper: IDisposable
    {
        Task<T> QuerySingleAsync<T>(string sql, DynamicParameters parameters,IDbTransaction? transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters parameters, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);
        Task<int> ExecuteAsync(string sql, DynamicParameters parameters, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }
}
