

using Microsoft.Data.SqlClient;
using System.Data;

namespace RestAPI.Example.Application.Database
{
    public interface IDBConnectionFactory
    {
        Task<IDbConnection> CreateAsync();
    }

    public class SqlConnectionFactory(string connectionString) : IDBConnectionFactory
    {
        public async Task<IDbConnection> CreateAsync()
        {
            var con = new SqlConnection(connectionString);
            await con.OpenAsync();
            return con;
        }
    }
}
