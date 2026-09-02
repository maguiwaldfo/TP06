using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace TP06.Data;

public class BD
{
    private readonly string _connectionString;

    public BD(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");
    }

    public IDbConnection CrearConexion()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<T>> Consultar<T>(string sql, object? parametros = null)
    {
        using var connection = CrearConexion();
        return await connection.QueryAsync<T>(sql, parametros);
    }

    public async Task<T?> ConsultarUno<T>(string sql, object? parametros = null)
    {
        using var connection = CrearConexion();
        return await connection.QuerySingleOrDefaultAsync<T>(sql, parametros);
    }

    public async Task<int> Ejecutar(string sql, object? parametros = null)
    {
        using var connection = CrearConexion();
        return await connection.ExecuteAsync(sql, parametros);
    }
}
