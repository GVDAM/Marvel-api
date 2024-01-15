using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Marvel.Core.Options;
using System.Data;

namespace Marvel.Infra.Context
{
    public class MarvelDbContext
    {
        private ConnectionStringOptions _connectionString;

        public MarvelDbContext(IOptionsMonitor<ConnectionStringOptions> connectionString)
        {
            _connectionString = connectionString.CurrentValue;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString.MarvelLocalDb);
    }
}
