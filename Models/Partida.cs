namespace TP06.Models;

public class Partida
{
    public int IdPartida { get; set; }
    public string NombreParticipante { get; set; } = string.Empty;
    public DateTime Inicio { get; set; }
    public DateTime Fin { get; set; }
    public int SalaActual { get; set; }
    public int VidasRestantes { get; set; }
    public bool Finalizado { get; set; }
    public bool Gano { get; set; }
}