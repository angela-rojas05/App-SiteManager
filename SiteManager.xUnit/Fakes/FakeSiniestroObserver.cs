using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.xUnit.Fakes
{
    public class FakeSiniestroObserver : ISiniestroObserver
    {
        public List<Siniestro> SiniestrosNotificados { get; } = new();

        public void Notificar(Siniestro siniestro)
        {
            SiniestrosNotificados.Add(siniestro);
        }
    }
}