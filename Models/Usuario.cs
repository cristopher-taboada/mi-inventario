using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace LOGIN.Models
{
    [Table("Usuarios")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string? Nombre { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("Password")]
        public string? Password { get; set; }

        [Column("Edad")]
        public int Edad { get; set; }

        [Column("Ciudad")]
        public string? Ciudad { get; set; }

        [Column("FechaRegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}