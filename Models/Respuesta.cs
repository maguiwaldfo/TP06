namespace TP06.Models;

public class Respuesta
{
    public int IdRespuesta { get; set; }
    public int IdPartida { get; set; }
    public int IdSala { get; set; }
    public string Intento { get; set; } = string.Empty;
    public bool EsCorrecto { get; set; }
}