using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.Infrastructure.Observers
{
    public class EmailObserver : ISiniestroObserver
    {
        public void Notificar(Siniestro siniestro)
        {
            Console.WriteLine($"[Email] Notificación enviada — Siniestro #{siniestro.Id}: {siniestro.TipoDanio} cambió a estado {siniestro.Estado}.");
        }
    }
}