using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LOGIN.Data;
using LOGIN.Models;
using System.Globalization;

namespace LOGIN.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reportes
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")))
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new ReportesViewModel();

            // ===== Resumen General =====
            viewModel.TotalProductos = await _context.Productos.CountAsync();
            viewModel.TotalUsuarios = await _context.Usuarios.CountAsync();
            viewModel.TotalPedidos = await _context.Pedidos.CountAsync();
            viewModel.TotalVentas = await _context.Pedidos.SumAsync(p => p.Total);

            // Productos con stock bajo (< 5)
            viewModel.ProductosBajoStock = await _context.Productos
                .CountAsync(p => p.Cantidad < 5 && p.Cantidad > 0);

            // ===== Productos más vendidos =====
            viewModel.ProductosMasVendidos = await _context.PedidoDetalles
                .Include(d => d.Producto)
                .GroupBy(d => d.ProductoId)
                .Select(g => new ProductoMasVendido
                {
                    Nombre = g.FirstOrDefault().Producto!.Nombre,
                    TotalVendido = g.Sum(d => d.Cantidad),
                    TotalRecaudado = g.Sum(d => d.Cantidad * d.PrecioUnitario)
                })
                .OrderByDescending(x => x.TotalVendido)
                .Take(10)
                .ToListAsync();

            // ===== Ventas por Mes =====
            var ventasPorMes = await _context.Pedidos
                .GroupBy(p => new { p.FechaPedido.Year, p.FechaPedido.Month })
                .Select(g => new
                {
                    Año = g.Key.Year,
                    Mes = g.Key.Month,
                    Total = g.Sum(p => p.Total),
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Año)
                .ThenByDescending(x => x.Mes)
                .Take(12)
                .ToListAsync();

            viewModel.VentasPorMes = ventasPorMes.Select(v => new VentasPorMes
            {
                Mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(v.Mes) + " " + v.Año,
                Total = v.Total,
                CantidadPedidos = v.Cantidad
            }).ToList();

            return View(viewModel);
        }
    }
}