using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class JuegoController : Controller
{
    public IActionResult Sala(int id)
    {
        Sala sala = BD.ObtenerSala(id);

        if (sala == null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(sala);
    }

    public IActionResult Victoria()
    {
        return View();
    }

    public IActionResult GameOver()
    {
        return View();
    }

    [HttpPost]
    public IActionResult EnviarRespuesta(int idSala, string respuesta)
    {
        Sala sala = BD.ObtenerSala(idSala);

        int idPartida = HttpContext.Session.GetInt32("PartidaId").Value;

        Partida partida = BD.ObtenerPartida(idPartida);

        if (respuesta == sala.Respuesta)
        {
            if (idSala == 5)
            {
                BD.GanarPartida(idPartida);
                return RedirectToAction("Victoria");
            }

            BD.ActualizarSalaActual(idPartida, idSala + 1);

            return RedirectToAction("Sala", new { id = idSala + 1 });
        }

        BD.RestarVida(idPartida);

        partida = BD.ObtenerPartida(idPartida);

        if (partida.Vidas <= 0)
        {
            BD.PerderPartida(idPartida);
            return RedirectToAction("GameOver");
        }

        ViewBag.Error = "Respuesta incorrecta. Te quedan "
                        + partida.Vidas + " vidas.";

        return View("Sala", sala);
    }
[HttpPost]
public IActionResult PerderVidaAhorcado(int idSala)
{
    int idPartida = HttpContext.Session.GetInt32("PartidaId").Value;

    BD.RestarVida(idPartida);

    Partida partida = BD.ObtenerPartida(idPartida);

    if (partida.Vidas <= 0)
    {
        BD.PerderPartida(idPartida);
        return RedirectToAction("GameOver");
    }

    TempData["Error"] = "Perdiste una vida en el ahorcado. Te quedan "
                        + partida.Vidas + " vidas.";

    return RedirectToAction("Sala", new { id = idSala });
}
}