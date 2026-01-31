using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTrabDWC.Models
{
    public class Obra
    {
        public int Id { get; set; }

        [Required, StringLength(250)]
        public string NomeObra { get; set; }

        [StringLength(2000)]
        public string DescricaoObra { get; set; }

        // FK para Cliente
        [Required]
        public int ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; }

        [StringLength(300)]
        public string Morada { get; set; }

        // Latitude / Longitude facultativos
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool Ativa { get; set; } = true;

        // Navegações: registos e mão-de-obra e pagamentos
        public ICollection<RegistoMaterial> RegistosMaterial { get; set; } = new List<RegistoMaterial>();
        public ICollection<RegistoMaoObra> RegistosMaoObra { get; set; } = new List<RegistoMaoObra>();
        public ICollection<RegistoPagamento> RegistosPagamento { get; set; } = new List<RegistoPagamento>();
    }
}
