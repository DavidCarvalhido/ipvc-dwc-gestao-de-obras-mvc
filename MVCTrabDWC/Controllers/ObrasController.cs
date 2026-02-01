using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTrabDWC.Data;
using MVCTrabDWC.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoObras.Controllers
{
    [Authorize]
    public class ObrasController : Controller
    {
        private readonly AppDbContext _context;

        public ObrasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Obras
        public async Task<IActionResult> Index()
        {
            var obras = await _context.Obras
                .Include(o => o.Cliente)
                .AsNoTracking()
                .OrderBy(o => o.NomeObra)
                .ToListAsync();

            return View(obras);
        }

        // GET: Obras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .Include(o => o.RegistosMaterial).ThenInclude(r => r.Material)
                .Include(o => o.RegistosMaoObra)
                .Include(o => o.RegistosPagamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (obra == null) return NotFound();

            return View(obra);
        }

        // GET: Obras/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = new SelectList(
                await _context.Clientes.OrderBy(c => c.Nome).ToListAsync(),
                "Id", "Nome"
            );

            return View();
        }

        // POST: Obras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Obra obra)
        {
            ModelState.Remove("Cliente");

            if (ModelState.IsValid)
            {
                _context.Add(obra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nome", obra.ClienteId);
            return View(obra);
        }

        // GET: Obras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nome", obra.ClienteId);
            return View(obra);
        }

        // POST: Obras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeObra,DescricaoObra,ClienteId,Morada,Latitude,Longitude,Ativa")] Obra obra)
        {
            if (id != obra.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Obras.Any(e => e.Id == obra.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nome", obra.ClienteId);
            return View(obra);
        }

        // GET: Obras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var obra = await _context.Obras
                .Include(o => o.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (obra == null) return NotFound();

            return View(obra);
        }

        // POST: Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obra = await _context.Obras.FindAsync(id);

            if (obra != null)
            {
                _context.Obras.Remove(obra);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
