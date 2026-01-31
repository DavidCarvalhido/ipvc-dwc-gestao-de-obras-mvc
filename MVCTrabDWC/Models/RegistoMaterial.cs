using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCTrabDWC.Models
{
    public enum OperacaoStock
    {
        ADD,
        REMOVE
    }

    public class RegistoMaterial
    {
        public int Id { get; set; }

        [Required]
        public int ObraId { get; set; }

        [ForeignKey(nameof(ObraId))]
        public Obra Obra { get; set; }

        [Required]
        public int MaterialId { get; set; }

        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        [Required]
        public OperacaoStock Operacao { get; set; }

        [Required]
        public DateTime DataHora { get; set; } = DateTime.Now;

        // Campo opcional para referência ou nota
        [StringLength(500)]
        public string Observacoes { get; set; }
    }
}
