## Nivel 3 — Componentes

- **¿Para quién es?** Para desarrolladores, líderes técnicos y arquitectos de software que necesitan entender cómo está estructurado el código fuente y cómo interactúan las clases principales del sistema. 

- **¿Qué muestra este diagrama?**  Hace un "zoom in" (lupa) a los contenedores principales de SiteManager (`Web`, `Application`, `Domain` e `Infrastructure`). Muestra los bloques de construcción internos como: controladores, servicios, interfaces y repositorios— detallando cómo se aplica la arquitectura limpia y qué patrones de diseño (como *Repository* y *Observer*) gobiernan el flujo de la aplicación.

```mermaid
C4Component
    title Diagrama de Componentes — SiteManager.Web y capas de soporte

    Person(usuario, "Usuario", "Técnico o supervisor")

    Container_Boundary(web, "SiteManager.Web") {
        Component(siniestroCtrl, "SiniestroController", "ASP.NET Core Controller", "Gestiona el CRUD de siniestros desde la interfaz web.")
        Component(clienteCtrl, "ClienteController", "ASP.NET Core Controller", "Gestiona el CRUD de clientes.")
        Component(evidenciaCtrl, "EvidenciaController", "ASP.NET Core Controller", "Gestiona el registro de evidencias por siniestro.")
        Component(cotizacionCtrl, "CotizacionController", "ASP.NET Core Controller", "Gestiona cotizaciones y presupuestos.")
        Component(materialCtrl, "MaterialController", "ASP.NET Core Controller", "Gestiona el inventario de materiales.")
        Component(reporteCtrl, "ReporteController", "ASP.NET Core Controller", "Gestiona los reportes técnicos de levantamiento.")
        Component(usuarioCtrl, "UsuarioController", "ASP.NET Core Controller", "Gestiona los usuarios del sistema.")
    }

    Container_Boundary(application, "SiteManager.Application") {
        Component(siniestroSvc, "SiniestroService", "C# Service — Patrón Observer", "Coordina operaciones de siniestros y notifica observers al actualizar.")
        Component(clienteSvc, "ClienteService", "C# Service", "Coordina operaciones de clientes.")
        Component(otrosSvc, "Otros Services", "C# Services", "EvidenciaService, CotizacionService, MaterialService, ReporteService, UsuarioService.")
    }

    Container_Boundary(domain, "SiteManager.Domain") {
        Component(interfaces, "Interfaces", "C# Interfaces — Patrón Repository", "ISiniestroRepository, IClienteRepository, ISiniestroObserver y demás contratos.")
        Component(models, "Models", "C# Classes", "Siniestro, Cliente, Evidencia, Cotizacion, Material, Reporte, Usuario.")
    }

    Container_Boundary(infrastructure, "SiteManager.Infrastructure") {
        Component(jsonRepos, "Json Repositories", "C# — Patrón Repository", "JsonSiniestroRepository, JsonClienteRepository y demás. Implementan las interfaces de Domain.")
        Component(emailObserver, "EmailObserver", "C# — Patrón Observer", "Notifica por consola cuando un siniestro cambia de estado.")
    }

    Rel(usuario, siniestroCtrl, "Usa", "HTTPS")
    Rel(siniestroCtrl, siniestroSvc, "Llama a", "C#")
    Rel(clienteCtrl, clienteSvc, "Llama a", "C#")
    Rel(siniestroSvc, interfaces, "Usa contrato", "C#")
    Rel(siniestroSvc, emailObserver, "Notifica", "C# Observer")
    Rel(interfaces, jsonRepos, "Implementado por", "C#")
    Rel(jsonRepos, models, "Opera sobre", "C#")
```