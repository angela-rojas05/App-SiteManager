using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManager.Domain.Models
{
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del material es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string UnidadMedida { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        public int CantidadStock { get; set; } = 0;

        [StringLength(100)]
        public string? Proveedor { get; set; }

        public int? SiniestroId { get; set; }
        public Siniestro? Siniestro { get; set; }
    }
}