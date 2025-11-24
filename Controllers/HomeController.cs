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

    // Método Index() unificado con lógica RF2 y RF7
    public async Task<IActionResult> Index()
    {
        var now = DateTime.Now;

        // --- Lógica RF2 (Notificaciones de Riego) ---
        var riegosPendientes = await _context.CalendarioDeRiego
            .Where(c => c.Fecha.Date == now.Date && c.Fecha > now)
            .OrderBy(c => c.Fecha)
            .ToListAsync();

        var notificaciones = riegosPendientes
            .Select(r => new NotificacionViewModel
            {
                // Incluimos la hora para que la notificación sea útil
                Mensaje = $"¡Riego Pendiente! Tipo: {r.Tipo} ({r.NivelAgua} ml). Programado para: {r.Fecha:HH:mm}."
            })
            .ToList();

        ViewBag.NotificacionesRiego = notificaciones;

        // --- Lógica RF7 (Visualización en Tiempo Real) ---

        // Obtener el último registro histórico por hora descendente
        var ultimoRegistro = await _context.Registro
            .OrderByDescending(r => r.Hora)
            .FirstOrDefaultAsync();

        if (ultimoRegistro != null)
        {
            var datosActuales = new DatosTiempoRealViewModel
            {
                Humedad = ultimoRegistro.Humedad,
                Temperatura = ultimoRegistro.Temperatura,
                HoraRegistro = ultimoRegistro.Hora
            };

            ViewBag.DatosTiempoReal = datosActuales;
        }
        else
        {
            ViewBag.DatosTiempoReal = null;
        }

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

    // View Model de Notificación de Riego (RF2)
    public class NotificacionViewModel
    {
        public string Mensaje { get; set; }
    }
}

// View Model de Datos de Tiempo Real (RF7)
// Definido fuera de la clase HomeController para que sea más fácil de usar en la vista.
public class DatosTiempoRealViewModel
{
    public decimal Humedad { get; set; }
    public decimal Temperatura { get; set; }
    public DateTime HoraRegistro { get; set; }
}