using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTrabDWC.Data;
using MVCTrabDWC.Models;

namespace MVCTrabDWC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var materiaisMaisUsados = _context.RegistosMaterial
                .GroupBy(r => r.Material.Nome)
                .Select(g => new DashboardMaterialChartItem
                {
                    Material = g.Key,
                    Quantidade = g.Sum(x => x.Quantidade)
                })
                .OrderByDescending(x => x.Quantidade)
                .Take(5)
                .ToList();

            

            var model = new DashboardViewModel
            {
                TotalClientes = _context.Clientes.Count(),
                TotalObras = _context.Obras.Count(),
                ObrasAtivas = _context.Obras.Count(o => o.Ativa),
                MateriaisBaixoStock = _context.Materiais.Count(m => m.StockDisponivel < 10),
                MateriaisMaisUsados = materiaisMaisUsados
            };

            //var ultimosMovimentos = _context.RegistosMaterial
            //    .Include(r => r.Obra)
            //        .ThenInclude(o => o.Cliente)
            //    .Include(r => r.Material)
            //    .AsNoTracking()
            //    .OrderByDescending(r => r.DataHora)
            //    .ToListAsync();

            return View(model);
        }
    }
}
