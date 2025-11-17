using Invernadero.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invernadero.Controllers
{
    public class CalendarioController : Controller
    {
        public readonly InvernaderoContext _context;

        public CalendarioController(InvernaderoContext context) { 
            _context = context;
        }

        public async Task <IActionResult> Create()
        { 
            return View(await _context.CalendarioDeRiego.ToListAsync());
        }

        /*public IActionResult Create()
        { 
            return View(new CalendarioDeRiego());
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Fecha, Tipo, NivelAgua")] CalendarioDeRiego calendario)
        {
            if (ModelState.IsValid)
            {
                _context.CalendarioDeRiego.Add(calendario);
                await _context.SaveChangesAsync();  
                return RedirectToAction(nameof(Create));
            }
            return View(calendario);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var calendario = await _context.CalendarioDeRiego.FindAsync(id);

            if (calendario == null)
                return NotFound();

            return View(calendario);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        { 
            if (id == null)
                return NotFound();

            var calendario = await _context.CalendarioDeRiego.FindAsync(id);
            Console.WriteLine(id);

            if (calendario == null)
                return NotFound();

            _context.CalendarioDeRiego.Remove(calendario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }
    }
}
