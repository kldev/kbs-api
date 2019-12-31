using Npgsql;

namespace KBS.Web.Services {
    public interface IConnectionFactory {
        NpgsqlConnection Connection();
    }
}
