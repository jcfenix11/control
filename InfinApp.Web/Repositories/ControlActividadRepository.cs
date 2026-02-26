using Microsoft.Data.SqlClient;
using System.Data;
using InfinApp.Web.Data;
using InfinApp.Web.Models;

namespace InfinApp.Web.Repositories
{
    public class ControlActividadRepository : IControlActividadRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ControlActividadRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // =============================
        // OBTENER TODOS
        // =============================
        public async Task<List<ControlActividad>> ObtenerTodos()
        {
            return await ObtenerPorColaborador(null); // Llama al método flexible
        }

        // =============================
        // OBTENER POR ID
        // =============================
        public async Task<ControlActividad?> ObtenerPorId(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand("ing.sp_control_actividades_obtener_por_id", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@cac_id", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Mapear(reader);
            }

            return null;
        }

        // =============================
        // CREAR
        // =============================
        public async Task<int> Crear(ControlActividad model)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand("sp_control_actividades_crear", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@cac_acl_id", model.ActividadesColaboradorId);
            command.Parameters.AddWithValue("@cac_fecha_inicio", model.FechaInicio);
            command.Parameters.AddWithValue("@cac_fecha_fin", (object?)model.FechaFin ?? DBNull.Value);
            command.Parameters.AddWithValue("@cac_corresponde", model.Corresponde);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        // =============================
        // ACTUALIZAR
        // =============================
        public async Task<bool> Actualizar(ControlActividad model)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand("sp_control_actividades_actualizar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@cac_id", model.Id);
            command.Parameters.AddWithValue("@cac_acl_id", model.ActividadesColaboradorId);
            command.Parameters.AddWithValue("@cac_fecha_inicio", model.FechaInicio);
            command.Parameters.AddWithValue("@cac_fecha_fin", (object?)model.FechaFin ?? DBNull.Value);
            command.Parameters.AddWithValue("@cac_corresponde", model.Corresponde);

            await connection.OpenAsync();

            var rows = await command.ExecuteNonQueryAsync();

            return rows > 0;
        }

        // =============================
        // ELIMINAR
        // =============================
        public async Task<bool> Eliminar(int id)
        {
            using var connection = _connectionFactory.CreateConnection();

            using var command = new SqlCommand("sp_control_actividades_eliminar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@cac_id", id);

            await connection.OpenAsync();

            var rows = await command.ExecuteNonQueryAsync();

            return rows > 0;
        }


        // =============================
        // OBTENER POR COLABORADOR
        // =============================
        // ================================
        // MÉTODO FLEXIBLE: FILTRAR POR COLABORADOR
        // ================================
        public async Task<List<ControlActividad>> ObtenerPorColaborador(int? colaboradorId)
        {
            var lista = new List<ControlActividad>();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("ing.sp_control_actividades_obtener", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Si colaboradorId es null, devuelve todos
            command.Parameters.AddWithValue("@cac_acl_id", (object?)colaboradorId ?? DBNull.Value);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(Mapear(reader));
            }
            return lista;
        }

        // =============================
        // MAPEO CENTRALIZADO
        // =============================
        private ControlActividad Mapear(SqlDataReader reader)
        {
            return new ControlActividad
            {
                Id = reader.GetInt64(reader.GetOrdinal("cac_id")),
                ActividadesColaboradorId = reader.GetInt64(reader.GetOrdinal("cac_acl_id")),
                FechaInicio = reader.GetDateTime(reader.GetOrdinal("cac_fecha_inicio")),
                FechaFin = reader.IsDBNull(reader.GetOrdinal("cac_fecha_fin"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("cac_fecha_fin")),
                Corresponde = reader.GetInt32(reader.GetOrdinal("cac_corresponde"))
            };
        }
    }
}