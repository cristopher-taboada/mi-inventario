using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace LOGIN.Models
{
    [Table("Usuarios")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email no válido")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }

        [Column("edad")]
        [Required(ErrorMessage = "La edad es requerida")]
        [Range(1, 120)]
        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Column("ciudad")]
        [Required(ErrorMessage = "La ciudad es requerida")]
        [Display(Name = "Ciudad")]
        public string? Ciudad { get; set; }

        [Column("fecharegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}