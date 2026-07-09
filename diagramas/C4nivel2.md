## Nivel 2 — Contenedores

**¿Para quién es este diagrama?** Para desarrolladores y arquitectos que necesitan entender las piezas técnicas grandes del sistema.

**¿Qué muestra este diagrama?** Las piezas técnicas principales que conforman SiteManager y cómo se comunican entre sí, es decir las capas implementadas en su arquitectura. Aquí ya aparecen tecnologías concretas como ASP.NET Core, pero sin entrar en detalles de clases o métodos internos.

```mermaid
C4Container
    title Diagrama de Contenedores — SiteManager

    %% Personas y Actores Externos
    Person(usuario, "Usuario Interno", "Técnico, supervisor, arquitecto o ingeniero.")
    Person(sistemaExterno, "Sistema Externo", "Consume la API REST.")

    %% Frontera del Sistema (SiteManager)
    System_Boundary(sitemanager, "SiteManager") {
        
        %% Capas de Presentación
        Container(web, "SiteManager.Web", "ASP.NET Core MVC", "Interfaz web con Razor Pages para gestionar siniestros, clientes, evidencias, cotizaciones, materiales, reportes y usuarios.")
        Container(api, "SiteManager.Api", "ASP.NET Core Web API", "Expone endpoints REST para el módulo de Siniestros. Documentado con Swagger.")

        %% Capas de Lógica y Dominio
        Container(application, "SiteManager.Application", "C# Class Library", "Contiene los servicios de negocio que coordinan las operaciones del sistema.")
        Container(domain, "SiteManager.Domain", "C# Class Library", "Define los modelos de entidades e interfaces de repositorios.")

        %% Capa de Infraestructura y Datos
        Container(infrastructure, "SiteManager.Infrastructure", "C# Class Library", "Implementa los repositorios JSON y los observers de notificación.")
        ContainerDb(data, "Archivos JSON", "JSON / Sistema de archivos", "Almacenamiento temporal de datos mientras se implementa MySQL.")
    }

    %% Relaciones de los Actores
    Rel(usuario, web, "Usa", "HTTPS / Navegador")
    Rel(sistemaExterno, api, "Consume", "HTTPS / JSON")

    %% Flujo de llamadas limpio hacia Application
    Rel_D(web, application, "Llama a", "C#")
    Rel_D(api, application, "Llama a", "C#")

    %% Distribución lateral para Domain e Infrastructure
    Rel_R(application, domain, "Usa", "C#")
    Rel_D(application, infrastructure, "Delega persistencia", "C#")
    
    %% Conexión final a Datos
    Rel_D(infrastructure, data, "Lee y escribe", "JSON")
```