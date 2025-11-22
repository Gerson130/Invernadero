using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invernadero.Models;
using Invernadero.ViewModels;

namespace Invernadero.Controllers
{
    public class SensorsController : Controller
    {
        private readonly InvernaderoContext _context;

        public SensorsController(InvernaderoContext context)
        {
            _context = context;
        }

        [HttpGet("api/sensors/registrartempohum")]
        public async Task<IActionResult> RegistrarTemperaturaHumedad([FromQuery] RegistroViewModel modelo)
        {
            if (modelo == null || modelo.SensorId <= 0)
            {
                return BadRequest("Datos de entrada inválidos o ID de Sensor no proporcionado");
            }

            var nuevoRegistro = new Registro
            {
                Humedad = modelo.Humedad,
                Temperatura = modelo.Temperatura,
                Hora = DateTime.Now
            };

            _context.Registro.Add(nuevoRegistro);
            await _context.SaveChangesAsync();

            var registroSensor = new RegistroSensor
            {
                IdRegistro = nuevoRegistro.Id,
                IdSensor = modelo.SensorId
            };

            _context.RegistroSensor.Add(registroSensor);

            await _context.SaveChangesAsync();

            string mensajeAlerta = "";

            if (modelo.Humedad < 75 || modelo.Humedad > 85)
            {
                mensajeAlerta += $"Humedad ({modelo.Humedad}%) fuera del rango óptimo (alrededor del 80%)";
            }
            if (modelo.Temperatura < 18 || modelo.Temperatura > 24)
            {
                mensajeAlerta += $"Temperatura ({modelo.Temperatura}°C) fuera del rango óptimo (18°C - 24°C)";
            }

            if (!string.IsNullOrEmpty(mensajeAlerta))
            {
                return Ok(new
                {
                    Status = "Datos guardados. ¡Alerta!",
                    Alerta = mensajeAlerta
                });
            }

            return Ok(new
            {
                Status = "Datos guardados correctamente",
                Humedad = modelo.Humedad,
                Temperatura = modelo.Temperatura
            });
        }

        // GET: Sensors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sensor.ToListAsync());
        }

        // GET: Sensors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // GET: Sensors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Ubicacion")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        // GET: Sensors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return View(sensor);
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Ubicacion")] Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorExists(sensor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        // GET: Sensors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // POST: Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor != null)
            {
                _context.Sensor.Remove(sensor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorExists(int id)
        {
            return _context.Sensor.Any(e => e.Id == id);
        }
    }
}

