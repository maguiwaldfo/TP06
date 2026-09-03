using Microsoft.AspNetCore.Mvc;
using TP06.Data;
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
    public IActionResult Identificacion()
    {

    return View();
    
    }

    [HttpPost]
    public IActionResult EnviarRespuesta(int idSala, string respuesta)
    {
        Sala sala = BD.ObtenerSala(idSala);

        if (respuesta == sala.Respuesta)
        {
            return RedirectToAction("Sala", new { id = idSala + 1 });
        }

        ViewBag.Error = "Respuesta incorrecta";
        return View("Sala", sala);
    }
}