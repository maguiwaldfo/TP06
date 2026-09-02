namespace TP06.Models;

public class Sala
{
    public int IdSala { get; set; }
    public string NombreSala { get; set; } = string.Empty;
    public int Orden { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string RespuestaEsperada { get; set; } = string.Empty;
}