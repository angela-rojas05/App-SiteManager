using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteManager.Models
{
    public enum EstadoSiniestro
    {
        Levantamiento,
        EnProceso,
        Cotizacion,
        Aprobado,
        EnReparacion,
        Finalizado,
        Cerrado
    }

    public class Siniestro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de daño es obligatorio")]
        [StringLength(150)]
        public string TipoDanio { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(250)]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Descripcion { get; set; }

        public EstadoSiniestro Estado { get; set; } = EstadoSiniestro.Levantamiento;

        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? FechaCierre { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public ICollection<Evidencia> Evidencias { get; set; } = new List<Evidencia>();
        public ICollection<Cotizacion> Cotizaciones { get; set; } = new List<Cotizacion>();
    }
}