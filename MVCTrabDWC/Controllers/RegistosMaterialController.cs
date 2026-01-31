using System;
using System.Linq;
using System.Threading.Tasks;
using MVCTrabDWC.Data;
using MVCTrabDWC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MVCTrabDWC.Controllers
{
    public class RegistosMaterialController
    {
    }
}

namespace GestaoObras.Controllers
{
    public class RegistosMaterialController : Controller
    {
        private readonly AppDbContext _context;

        public RegistosMaterialController(AppDbContext context)
        {
            _context = context;
        }

        // POST: RegistosMaterial/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int obraId, int materialId, int quantidade, string observacoes)
        {
            if (quantidade <= 0)
            {
                ModelState.AddModelError(string.Empty, "A quantidade deve ser maior que zero.");
            }

            var obra = await _context.Obras.FindAsync(obraId);
            if (obra == null) ModelState.AddModelError(string.Empty, "Obra não encontrada.");

            var material = await _context.Materiais.FindAsync(materialId);
            if (material == null) ModelState.AddModelError(string.Empty, "Material não encontrado.");

            if (!ModelState.IsValid)
            {
                // Recarregar dados para partial view (será usada via AJAX ou form normal)
                ViewBag.Materiais = await _context.Materiais.OrderBy(m => m.Nome).ToListAsync();
                return PartialView("~/Views/Obras/_RegistoMaterial.cshtml", new RegistoMaterial { ObraId = obraId });
            }

            // Verificar stock disponível
            if (material.StockDisponivel < quantidade)
            {
                ModelState.AddModelError(string.Empty, $"Stock insuficiente. Disponível: {material.StockDisponivel}");
                ViewBag.Materiais = await _context.Materiais.OrderBy(m => m.Nome).ToListAsync();
                return PartialView("~/Views/Obras/_RegistoMaterial.cshtml", new RegistoMaterial { ObraId = obraId });
            }

            // Criar registo (Operacao = REMOVE)
            var reg = new RegistoMaterial
            {
                ObraId = obraId,
                MaterialId = materialId,
                Quantidade = quantidade,
                Operacao = OperacaoStock.REMOVE,
                DataHora = DateTime.UtcNow,
                Observacoes = observacoes
            };

            // Atualizar stock
            material.StockDisponivel -= quantidade;

            _context.RegistosMaterial.Add(reg);
            _context.Materiais.Update(material);

            await _context.SaveChangesAsync();

            // Redireciona para detalhes da obra (ou devolve partial)
            return RedirectToAction("Details", "Obras", new { id = obraId });
        }
    }
}
