

using Microsoft.Data.SqlClient;
using System.Data;

namespace RestAPI.Example.Application.Database
{
    public interface IDBConnectionFactory
    {
        Task<IDbConnection> CreateAsync(CancellationToken cancellationToken = default);
    }

    public class SqlConnectionFactory(string connectionString) : IDBConnectionFactory
    {
        public async Task<IDbConnection> CreateAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var con = new SqlConnection(connectionString);
            await con.OpenAsync();
            return con;
        }
    }
}
