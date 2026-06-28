using SiteManager.Domain.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface ISiniestroObserver
    {
        void Notificar(Siniestro siniestro);
    }
}