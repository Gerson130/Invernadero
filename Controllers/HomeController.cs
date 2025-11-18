using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Invernadero.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Invernadero.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly InvernaderoContext _context;

    public HomeController(ILogger<HomeController> logger, InvernaderoContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var now = DateTime.Now;

        var riegosPendientes = await _context.CalendarioDeRiego
            .Where(c => c.Fecha.Date == now.Date && c.Fecha > now)
            .OrderBy(c => c.Fecha)
            .ToListAsync();

        var notificaciones = riegosPendientes
            .Select(r => new NotificacionViewModel
            {
                Mensaje = $"Riego pendiente, Tipo: {r.Tipo} ({r.NivelAgua} ml)"
            })
            .ToList();

        ViewBag.NotificacionesRiego = notificaciones;

        return View();
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

public class NotificacionViewModel
{
    public string Mensaje { get; set; }
}
