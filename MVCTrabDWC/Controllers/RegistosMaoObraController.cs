using MVCTrabDWC.Data;
using MVCTrabDWC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MVCTrabDWC.Controllers
{
    public class RegistosMaoObraController : Controller
    {
        private readonly AppDbContext _context;

        public RegistosMaoObraController(AppDbContext context)
        {
            _context = context;
        }

        // POST: RegistosMaoObra/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int obraId, string nomePessoa, double horasTrabalhadas, string observacoes)
        {
            if (string.IsNullOrWhiteSpace(nomePessoa)) ModelState.AddModelError(string.Empty, "Nome da pessoa é obrigatório.");
            if (horasTrabalhadas <= 0) ModelState.AddModelError(string.Empty, "Horas deve ser maior que 0.");

            var obra = await _context.Obras.FindAsync(obraId);
            if (obra == null) ModelState.AddModelError(string.Empty, "Obra não encontrada.");

            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Obras/_RegistoMaoObra.cshtml", new RegistoMaoObra { ObraId = obraId });
            }

            var reg = new RegistoMaoObra
            {
                ObraId = obraId,
                NomePessoa = nomePessoa,
                HorasTrabalhadas = horasTrabalhadas,
                DataHora = DateTime.UtcNow,
                Observacoes = observacoes
            };

            _context.RegistosMaoObra.Add(reg);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Obras", new { id = obraId });
        }
    }
}
