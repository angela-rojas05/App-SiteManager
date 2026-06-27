using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManager.Models
{
    public class Evidencia
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string? Titulo { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public string RutaArchivo { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime FechaSubida { get; set; } = DateTime.Now;

        [ForeignKey("Siniestro")]
        public int SiniestroId { get; set; }
        public Siniestro Siniestro { get; set; } = null!;
    }
}