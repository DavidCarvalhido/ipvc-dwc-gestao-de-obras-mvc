using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTrabDWC.Data;

namespace MVCTrabDWC.Controllers
{
    [Authorize]
    public class MovimentosController : Controller
    {
        private readonly AppDbContext _context;

        public MovimentosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Movimentos
        public async Task<IActionResult> Index()
        {
            var movimentos = await _context.RegistosMaterial
                .Include(r => r.Obra)
                    .ThenInclude(o => o.Cliente)
                .Include(r => r.Material)
                .AsNoTracking()
                .OrderByDescending(r => r.DataHora)
                .ToListAsync();

            return View(movimentos);
        }
    }
}
