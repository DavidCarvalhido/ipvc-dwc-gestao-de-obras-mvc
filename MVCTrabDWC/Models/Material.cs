using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCTrabDWC.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Nome { get; set; }

        [StringLength(1000)]
        public string Descricao { get; set; }

        // Stock disponível actual
        public int StockDisponivel { get; set; }

        // Navegação: registos de movimentos associados
        public ICollection<RegistoMaterial> RegistosMaterial { get; set; } = new List<RegistoMaterial>();
    }
}
