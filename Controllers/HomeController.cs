using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Historia()
    {  
        return View();

    }
    public IActionResult Integrantes()
    {

    return View();

    }

 [HttpPost]
    public IActionResult Comenzar(string nombre)
    { 
    if (nombre == null || nombre == "")
    {
        ViewBag.Error = "Tenés que ingresar tu nombre";
        return View("Index");
    }

    BD.CrearPartida(nombre);

    Partida partida = BD.ObtenerUltimaPartida(nombre);

    HttpContext.Session.SetInt32("PartidaId", partida.IdPartida);
    HttpContext.Session.SetString("Nombre", nombre);

    return RedirectToAction("Sala", "Juego", new { id = 1 });
     }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
