using Microsoft.Data.SqlClient;


namespace InfinApp.Web.Data
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection")
                ?? throw new InvalidOperationException("La cadena de conexión 'defaultConnection' no está configurada.");
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}