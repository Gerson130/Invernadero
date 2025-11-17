using Invernadero.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invernadero.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly InvernaderoContext _context;

        public UsuarioController(InvernaderoContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Get(string nombre, string contrasenia)
        {
            if (contrasenia == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Contrasenia == contrasenia);
            if (usuario == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Calendario");
        }
    }
}