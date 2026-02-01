using Microsoft.AspNetCore.Mvc;

namespace MVCTrabDWC.Models
{
    public class MovimentoStockResumoViewModel
    {
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string Operacao { get; set; } // ADD / REMOVE
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
    }
}
