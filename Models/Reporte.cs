using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManager.Models
{
    public class Reporte
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título del reporte es obligatorio")]
        [StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Contenido { get; set; }

        [StringLength(50)]
        public string? TipoReporte { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaGeneracion { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string? GeneradoPor { get; set; }

        [ForeignKey("Siniestro")]
        public int SiniestroId { get; set; }
        public Siniestro Siniestro { get; set; } = null!;
    }
}