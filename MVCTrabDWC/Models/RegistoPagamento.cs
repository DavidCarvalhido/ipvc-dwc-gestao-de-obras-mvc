using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTrabDWC.Models
{
    public class RegistoPagamento
    {
        public int Id { get; set; }

        [Required]
        public int ObraId { get; set; }

        [ForeignKey(nameof(ObraId))]
        public Obra Obra { get; set; }

        [Required, StringLength(200)]
        public string NomePessoa { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataHora { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Observacoes { get; set; }
    }
}
