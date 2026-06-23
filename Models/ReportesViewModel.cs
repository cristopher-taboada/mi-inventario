using LOGIN.Models;

namespace LOGIN.Models
{
    public class ReportesViewModel
    {
        // Resumen general
        public int TotalProductos { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalPedidos { get; set; }
        public decimal TotalVentas { get; set; }
        public int ProductosBajoStock { get; set; }

        // Productos más vendidos
        public List<ProductoMasVendido>? ProductosMasVendidos { get; set; }

        // Ventas por mes
        public List<VentasPorMes>? VentasPorMes { get; set; }
    }

    public class ProductoMasVendido
    {
        public string? Nombre { get; set; }
        public int TotalVendido { get; set; }
        public decimal TotalRecaudado { get; set; }
    }

    public class VentasPorMes
    {
        public string? Mes { get; set; }
        public decimal Total { get; set; }
        public int CantidadPedidos { get; set; }
    }
}