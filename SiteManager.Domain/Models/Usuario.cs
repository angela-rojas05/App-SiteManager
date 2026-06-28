using System.ComponentModel.DataAnnotations;

namespace SiteManager.Domain.Models
{
    public enum RolUsuario
    {
        Administrador,
        Arquitecto,
        Ingeniero,
        Tecnico,
        Supervisor
    }

    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(150)]
        public string NombreCompleto { get; set; } = string.Empty;

        [EmailAddress]
        public string? Correo { get; set; }

        [Phone]
        public string? Telefono { get; set; }

        public RolUsuario Rol { get; set; } = RolUsuario.Tecnico;

        public bool Activo { get; set; } = true;

        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}