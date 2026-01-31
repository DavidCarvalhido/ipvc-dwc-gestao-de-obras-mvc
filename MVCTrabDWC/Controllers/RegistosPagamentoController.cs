using MVCTrabDWC.Data;
using MVCTrabDWC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MVCTrabDWC.Controllers
{
    public class RegistosPagamentoController : Controller
    {
        private readonly AppDbContext _context;

        public RegistosPagamentoController(AppDbContext context)
        {
            _context = context;
        }

        // POST: RegistosPagamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int obraId, string nomePessoa, decimal valor, string observacoes)
        {
            if (string.IsNullOrWhiteSpace(nomePessoa)) ModelState.AddModelError(string.Empty, "Nome da pessoa é obrigatório.");
            if (valor <= 0) ModelState.AddModelError(string.Empty, "Valor deve ser maior que 0.");

            var obra = await _context.Obras.FindAsync(obraId);
            if (obra == null) ModelState.AddModelError(string.Empty, "Obra não encontrada.");

            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Obras/_RegistoPagamentos.cshtml", new RegistoPagamento { ObraId = obraId });
            }

            var reg = new RegistoPagamento
            {
                ObraId = obraId,
                NomePessoa = nomePessoa,
                Valor = valor,
                DataHora = DateTime.UtcNow,
                Observacoes = observacoes
            };

            _context.RegistosPagamento.Add(reg);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Obras", new { id = obraId });
        }
    }
}
