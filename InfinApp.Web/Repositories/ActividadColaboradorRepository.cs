using InfinApp.Web.Data;
using InfinApp.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InfinApp.Web.Repositories
{
    public class ActividadColaboradorRepository : IActividadColaboradorRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public ActividadColaboradorRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<ActividadColaborador>> ObtenerTodos()
        {
            var lista = new List<ActividadColaborador>();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("gen.sp_actividad_colaborador_obtener_todos", connection);
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(Mapear(reader));
            }

            return lista;
        }

        public async Task<ActividadColaborador?> ObtenerPorId(long id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("gen.sp_actividad_colaborador_obtener_por_id", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@acl_id", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return Mapear(reader);
            }
            return null;
        }

        public async Task<ActividadColaborador?> Crear(ActividadColaborador model)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("gen.sp_actividad_colaborador_crear", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@acl_actividad", model.Actividad);
            command.Parameters.AddWithValue("@acl_descripcion", (object?)model.Descripcion ?? DBNull.Value);
            command.Parameters.AddWithValue("@acl_estatus", model.Estatus ?? true);
            command.Parameters.AddWithValue("@acl_rol", (object?)model.Rol ?? DBNull.Value);
            command.Parameters.AddWithValue("@acl_fecha_creada", model.FechaCracion);
            command.Parameters.AddWithValue("@acl_categoria", (object?)model.Categoria ?? DBNull.Value);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Mapear(reader);
            }

            return null;
        }

        public async Task<ActividadColaborador?> Actualizar(ActividadColaborador model)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("gen.sp_actividad_colaborador_actualizar", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@acl_id", model.Id);
            command.Parameters.AddWithValue("@acl_actividad", model.Actividad ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@acl_descripcion", model.Descripcion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@acl_estatus", model.Estatus ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@acl_rol", model.Rol ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@acl_categoria", model.Categoria ?? (object)DBNull.Value);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Mapear(reader);
            }

            return null;
        }

        public async Task<bool> Eliminar(long id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("gen.sp_actividad_colaborador_eliminar", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@acl_id", SqlDbType.BigInt).Value = id;

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            if (result == null)
                return false;

            return Convert.ToInt32(result) > 0;
        }

        private ActividadColaborador Mapear(SqlDataReader reader)
        {
            return new ActividadColaborador
            {
                Id = reader.GetInt64(reader.GetOrdinal("acl_id")),
                Actividad = reader.GetString(reader.GetOrdinal("acl_actividad")),
                Descripcion = reader.IsDBNull(reader.GetOrdinal("acl_descripcion"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("acl_descripcion")),
                Estatus = reader.IsDBNull(reader.GetOrdinal("acl_estatus"))
                    ? null
                    : reader.GetBoolean(reader.GetOrdinal("acl_estatus")),
                Rol = reader.IsDBNull(reader.GetOrdinal("acl_rol"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("acl_rol")),                
                Categoria = reader.IsDBNull(reader.GetOrdinal("acl_categoria"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("acl_categoria")),
            };
        }
    }
}
