using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace LOGIN.Models
{
    [Table("Productos")]
    public class Producto : BaseModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("Descripcion")]
        public string? Descripcion { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Column("Precio")]
        public decimal Precio { get; set; }

        [Column("FechaRegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}