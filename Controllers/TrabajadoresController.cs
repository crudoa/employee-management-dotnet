using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyperPrueba.Data;
using MyperPrueba.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic; // Required for Enumerable.Empty

namespace MyperPrueba.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrabajadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index(string sexFilter)
        {
            IQueryable<Trabajador> trabajadoresQuery = _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito);

            if (!string.IsNullOrEmpty(sexFilter))
            {
                // Asegurarse de que el filtro de sexo coincida con los valores 'M' o 'F' de la DB
                trabajadoresQuery = trabajadoresQuery.Where(t => t.Sexo == sexFilter);
            }

            ViewBag.SexFilter = sexFilter; // Para mantener el filtro seleccionado en la vista
            return View(await trabajadoresQuery.ToListAsync());
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["IdProvincia"] = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "NombreProvincia"); // Vacío inicialmente
            ViewData["IdDistrito"] = new SelectList(Enumerable.Empty<SelectListItem>(), "Id", "NombreDistrito"); // Vacío inicialmente
            return View();
        }

        // POST: Trabajadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            // El campo Id se ha eliminado del Bind porque es auto-generado por la DB.
            // EF Core lo manejará automáticamente.

            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Recargar SelectLists en caso de error de validación
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajador.IdDepartamento);
            ViewData["IdProvincia"] = new SelectList(_context.Provincias.Where(p => p.IdDepartamento == trabajador.IdDepartamento), "Id", "NombreProvincia", trabajador.IdProvincia);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos.Where(d => d.IdProvincia == trabajador.IdProvincia), "Id", "NombreDistrito", trabajador.IdDistrito);
            return View(trabajador);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }
            // Recargar SelectLists para la vista de edición
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajador.IdDepartamento);
            ViewData["IdProvincia"] = new SelectList(_context.Provincias.Where(p => p.IdDepartamento == trabajador.IdDepartamento), "Id", "NombreProvincia", trabajador.IdProvincia);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos.Where(d => d.IdProvincia == trabajador.IdProvincia), "Id", "NombreDistrito", trabajador.IdDistrito);
            return View(trabajador);
        }

        // POST: Trabajadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (id != trabajador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorExists(trabajador.Id))
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
            // Recargar SelectLists en caso de error de validación
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajador.IdDepartamento);
            ViewData["IdProvincia"] = new SelectList(_context.Provincias.Where(p => p.IdDepartamento == trabajador.IdDepartamento), "Id", "NombreProvincia", trabajador.IdProvincia);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos.Where(d => d.IdProvincia == trabajador.IdProvincia), "Id", "NombreDistrito", trabajador.IdDistrito);
            return View(trabajador);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadorExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }

        // Métodos para cargar provincias y distritos dinámicamente
        [HttpGet]
        public JsonResult GetProvincias(int idDepartamento)
        {
            var provincias = _context.Provincias
                                     .Where(p => p.IdDepartamento == idDepartamento)
                                     .Select(p => new { value = p.Id, text = p.NombreProvincia })
                                     .ToList();
            return Json(provincias);
        }

        [HttpGet]
        public JsonResult GetDistritos(int idProvincia)
        {
            var distritos = _context.Distritos
                                    .Where(d => d.IdProvincia == idProvincia)
                                    .Select(d => new { value = d.Id, text = d.NombreDistrito })
                                    .ToList();
            return Json(distritos);
        }
    }
}
