using Microsoft.AspNetCore.Mvc;

namespace MVCTrabDWC.Models
{
    public class DashboardViewModel
    {
        public int TotalClientes { get; set; }
        public int TotalObras { get; set; }
        public int ObrasAtivas { get; set; }
        public int MateriaisBaixoStock { get; set; }
        public List<DashboardMaterialChartItem> MateriaisMaisUsados { get; set; } = new();
        //public List<MovimentoStockResumoViewModel> UltimosMovimentos { get; set; } = new();
    }
}
