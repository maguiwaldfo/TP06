using Dapper;
using Microsoft.Data.SqlClient;
using TP06.Models;

public static class BD
{
    private static string _connectionString =
        @"Server=localhost;Database=Sala;Trusted_Connection=True;TrustServerCertificate=True;";


    public static List<Sala> ObtenerSalas()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT
                    idSala AS Id,
                    nombreSala AS Nombre,
                    orden AS Orden,
                    descripcion AS Descripcion,
                    respuestaEsperada AS Respuesta,
                    imagenes AS Imagenes
                FROM [dbo].[salas]
                ORDER BY orden";

            return connection.Query<Sala>(sql).ToList();
        }
    }


    public static Sala ObtenerSala(int idSala)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT
                    idSala AS Id,
                    nombreSala AS Nombre,
                    orden AS Orden,
                    descripcion AS Descripcion,
                    respuestaEsperada AS Respuesta,
                    imagenes AS Imagenes
                FROM [dbo].[salas]
                WHERE idSala = @idSala";

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
                SELECT
                    idPista AS IdPista,
                    idSala AS IdSala,
                    numeroPista AS NumeroPista,
                    texto AS Texto
                FROM [dbo].[pista]
                WHERE idSala = @idSala
                ORDER BY numeroPista";

            return connection.Query<Pista>(
                sql,
                new { idSala }
            ).ToList();
        }
    }

    public static int ObtenerNuevoIdPartida()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT ISNULL(MAX(idPartida), 0) + 1
                FROM [dbo].[partida]";

            return connection.QueryFirstOrDefault<int>(sql);
        }
    }


    public static void CrearPartida(string nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            int nuevoId = ObtenerNuevoIdPartida();

            string sql = @"
                INSERT INTO [dbo].[partida]
                (
                    idPartida,
                    nombreParticipante,
                    inicio,
                    fin,
                    salaActual,
                    vidasRestantes,
                    finalizado,
                    gano
                )
                VALUES
                (
                    @idPartida,
                    @nombre,
                    @inicio,
                    @fin,
                    @salaActual,
                    @vidas,
                    @finalizado,
                    @gano
                )";

            connection.Execute(
                sql,
                new
                {
                    idPartida = nuevoId,
                    nombre = nombre,
                    inicio = DateTime.Now,
                    fin = DateTime.Now,
                    salaActual = 1,
                    vidas = 3,
                    finalizado = false,
                    gano = false
                }
            );
        }
    }


    public static Partida ObtenerUltimaPartida(string nombre)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                SELECT TOP 1
                    idPartida AS IdPartida,
                    nombreParticipante AS Nombre,
                    inicio AS Inicio,
                    vidasRestantes AS Vidas,
                    finalizado AS Fin,
                    gano AS Gano
                FROM [dbo].[partida]
                WHERE nombreParticipante = @nombre
                ORDER BY idPartida DESC";

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
                SELECT
                    idPartida AS IdPartida,
                    nombreParticipante AS Nombre,
                    inicio AS Inicio,
                    vidasRestantes AS Vidas,
                    finalizado AS Fin,
                    gano AS Gano
                FROM [dbo].[partida]
                WHERE idPartida = @idPartida";

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
                INSERT INTO [dbo].[respuestas]
                (idRespuesta, idPartida, idSala, intento, esCorrecto)
                VALUES
                (@IdRespuesta, @IdPartida, @IdSala, @Intento, @EsCorrecto)";

            connection.Execute(sql, respuesta);
        }
    }


    public static void RestarVida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE [dbo].[partida]
                SET vidasRestantes = vidasRestantes - 1
                WHERE idPartida = @idPartida";

            connection.Execute(
                sql,
                new { idPartida }
            );
        }
    }


    public static void ActualizarSalaActual(int idPartida, int idSala)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE [dbo].[partida]
                SET salaActual = @idSala
                WHERE idPartida = @idPartida";

            connection.Execute(
                sql,
                new
                {
                    idPartida,
                    idSala
                }
            );
        }
    }


    public static void GanarPartida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE [dbo].[partida]
                SET fin = @fin,
                    finalizado = 1,
                    gano = 1
                WHERE idPartida = @idPartida";

            connection.Execute(
                sql,
                new
                {
                    idPartida,
                    fin = DateTime.Now
                }
            );
        }
    }


    public static void PerderPartida(int idPartida)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = @"
                UPDATE [dbo].[partida]
                SET fin = @fin,
                    finalizado = 1,
                    gano = 0
                WHERE idPartida = @idPartida";

            connection.Execute(
                sql,
                new
                {
                    idPartida,
                    fin = DateTime.Now
                }
            );
        }
    }
}