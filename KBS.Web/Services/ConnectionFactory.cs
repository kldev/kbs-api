using Npgsql;

namespace KBS.Web.Services
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection Connection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}