using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManager.Domain.Models
{
    public class Cotizacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Concepto { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Materiales { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostoEstimado { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostoFinal { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaCotizacion { get; set; } = DateTime.Now;

        [ForeignKey("Siniestro")]
        public int SiniestroId { get; set; }
        public Siniestro Siniestro { get; set; } = null!;
    }
}