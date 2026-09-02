
using Dapper;
using Microsoft.Data.SqlClient;
using TP06.Models;

public static class BD
{
    private static string _connectionString =
        @"Server=localhost;Database=TP06;Trusted_Connection=True;TrustServerCertificate=True;";


    public static List<Sala> ObtenerSalas()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Salas ORDER BY Orden";

            return connection.Query<Sala>(sql).ToList();
        }
    }


    public static Sala ObtenerSala(int idSala)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Salas WHERE Id = @idSala";

            return connection.QueryFirstOrDefault<Sala>(
                sql,
                new { idSala }
            );
        }
    }


    public static List<Pista> ObtenerPistas(int idSala)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT *
                FROM Pistas
                WHERE IdSala = @idSala
                ORDER BY NumeroPista";

            return connection.Query<Pista>(
                sql,
                new { idSala }
            ).ToList();
        }
    }


    public static void CrearPartida(string nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                INSERT INTO Partidas
                (Nombre, Inicio, Vidas, Fin, Gano)
                VALUES
                (@nombre, @inicio, 3, 0, 0)";

            connection.Execute(
                sql,
                new
                {
                    nombre = nombre,
                    inicio = DateTime.Now
                }
            );
        }
    }


    public static Partida ObtenerUltimaPartida(string nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT TOP 1 *
                FROM Partidas
                WHERE Nombre = @nombre
                ORDER BY Id DESC";

            return connection.QueryFirstOrDefault<Partida>(
                sql,
                new { nombre }
            );
        }
    }


    public static Partida ObtenerPartida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT *
                FROM Partidas
                WHERE Id = @idPartida";

            return connection.QueryFirstOrDefault<Partida>(
                sql,
                new { idPartida }
            );
        }
    }

    public static void GuardarRespuesta(Respuesta respuesta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                INSERT INTO Respuestas
                (IdPartida, IdSala, Intento, Correcta)
                VALUES
                (@IdPartida, @IdSala, @Intento, @Correcta)";

            connection.Execute(sql, respuesta);
        }
    }


    public static void RestarVida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE Partidas
                SET Vidas = Vidas - 1
                WHERE Id = @idPartida";

            connection.Execute(
                sql,
                new { idPartida }
            );
        }
    }



    public static void GanarPartida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE Partidas
                SET Fin = 1,
                    Gano = 1
                WHERE Id = @idPartida";

            connection.Execute(
                sql,
                new { idPartida }
            );
        }
    }


    public static void PerderPartida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE Partidas
                SET Fin = 1,
                Gano = 0
                WHERE Id = @idPartida";

            connection.Execute(
                sql,
                new { idPartida }
            );
        }
    }
}