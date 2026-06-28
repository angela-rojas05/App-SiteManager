using System.ComponentModel.DataAnnotations;
namespace SiteManager.Domain.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Apellido { get; set; }

        [Phone]
        public string? Telefono { get; set; }

        [EmailAddress]
        public string? Correo { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
    }
}