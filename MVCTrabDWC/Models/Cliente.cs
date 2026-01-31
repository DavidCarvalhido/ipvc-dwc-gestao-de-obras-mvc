using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCTrabDWC.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Nome { get; set; }

        [Required, StringLength(20)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O NIF deve ter exatamente 9 dígitos")]
        public string NIF { get; set; }

        [StringLength(300)]
        public string Morada { get; set; }

        [EmailAddress, StringLength(200)]
        public string Email { get; set; }

        [Phone, StringLength(50)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O telefone deve ter exatamente 9 dígitos")]
        public string Telefone { get; set; }

        // Navegação: um cliente pode ter várias obras
        public ICollection<Obra> Obras { get; set; } = new List<Obra>();
    }
}
