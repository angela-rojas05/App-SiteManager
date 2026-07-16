# ADR-06: SiteManager — Deuda Técnica Identificada

| Campo  | Valor |
|--------|-------|
| Autor  | Ángela Rojas |
| Fecha  | 15/07/2026 |
| Estado | `APROBADO` |

---

## Contexto

Durante el desarrollo de SiteManager a lo largo del cuatrimestre, se tomaron algunas decisiones rápidas para poder avanzar y cumplir con las entregas. Algunas de esas decisiones dejaron partes del sistema funcionando pero de una forma que no es sostenible a largo plazo. A eso se le llama deuda técnica: no es necesariamente un error, sino algo que se sabe que hay que mejorar más adelante. Documentarlas aquí permite tener claridad sobre qué existe, por qué existe y qué habría que hacer para resolverlo.

---

## Deuda 1 — La dirección de los archivos de datos está escrita fija en el código

**¿Qué es?**
SiteManager guarda su información en archivos JSON dentro de una carpeta llamada `data`. La dirección de esa carpeta está escrita directamente dentro del código de la aplicación, en el archivo `Program.cs`, tanto del proyecto Web como del proyecto Api:

```csharp
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "data");
```

Esto significa que si alguien necesita cambiar dónde se guardan los archivos, tiene que abrir el código, encontrar esa línea, cambiarla y volver a compilar todo el proyecto. No existe ninguna variable de configuración externa que controle esta ruta.

**¿Por qué existe?**
Se tomó esta decisión de forma consciente para avanzar más rápido durante el desarrollo. Como el proyecto siempre corrió en la misma computadora y la carpeta nunca cambió de lugar, escribir la dirección fija funcionó sin ningún problema durante todo el cuatrimestre.

**¿Qué pasa si no se resuelve?**
Si en algún momento el proyecto se mueve a otro servidor, se organiza de forma diferente, o se necesita que el ambiente de pruebas y el ambiente real usen carpetas distintas, no hay forma de cambiarlo sin tocar el código directamente. Esto rompe el principio de separación entre configuración y código, haciendo al sistema frágil y difícil de mover o adaptar.

**¿Cómo se resolvería?**
La solución es sacar esa dirección del código y ponerla en el archivo de configuración `appsettings.json`, que es el lugar correcto para guardar este tipo de información:

```json
"DataSettings": {
    "DataPath": "data"
}
```

Y leerla desde el código así:

```csharp
var dataPath = builder.Configuration["DataSettings:DataPath"]
    ?? Path.Combine(builder.Environment.ContentRootPath, "data");
```

Así, cambiar la ruta sería tan simple como editar un archivo de configuración, sin necesidad de tocar ni recompilar el código.

---

## Deuda 2 — El sistema de notificaciones no envía nada real

**¿Qué es?**
En el ADR-05 se implementó el patrón Observer para que cada vez que un siniestro cambie de estado, el sistema notifique a los involucrados. Sin embargo, la notificación actual solo imprime un mensaje en la consola de la computadora donde corre la aplicación:

```csharp
Console.WriteLine($"[Email] Notificación enviada — Siniestro #{siniestro.Id}: {siniestro.TipoDanio} cambió a estado {siniestro.Estado}.");
```

Nadie externo recibe nada — ni un correo, ni un mensaje, ni ningún tipo de aviso real.

**¿Por qué existe?**
El objetivo principal del ADR-05 era demostrar que el patrón Observer estaba bien aplicado a nivel de estructura y arquitectura. Conectar un servicio real de envío de correos requería configurar herramientas externas que estaban fuera del alcance de las entregas del cuatrimestre, así que se dejó como simulación para no bloquear el avance.

**¿Qué pasa si no se resuelve?**
Si el sistema llegara a usarse en un entorno real, los técnicos y supervisores nunca sabrían cuándo un siniestro cambia de estado. El patrón Observer estaría bien construido por dentro, pero completamente inútil por fuera. El valor de haberlo implementado quedaría en cero desde el punto de vista del usuario.

**¿Cómo se resolvería?**
La solución sería reemplazar el `Console.WriteLine` por una implementación real que use un servicio de envío de correos como SendGrid o el cliente SMTP de .NET. El patrón Observer ya está en su lugar — lo único que cambiaría es lo que hace el observer por dentro cuando recibe la notificación. No habría que tocar ni los controladores ni los servicios, solo la clase `EmailObserver` en la capa de Infrastructure:

```csharp
public class EmailObserver : ISiniestroObserver
{
    private readonly IEmailService _emailService;

    public EmailObserver(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void Notificar(Siniestro siniestro)
    {
        _emailService.Enviar(
            destinatario: "supervisor@sitemanager.com",
            asunto: $"Siniestro #{siniestro.Id} actualizado",
            cuerpo: $"El siniestro '{siniestro.TipoDanio}' cambió a estado {siniestro.Estado}."
        );
    }
}
```

---

## Deuda 3 — Almacenamiento en archivos JSON en lugar de base de datos real

**¿Qué es?**
SiteManager actualmente guarda toda su información en archivos JSON dentro de una carpeta `data/`. Hay un archivo por cada módulo del sistema: `siniestros.json`, `clientes.json`, `evidencias.json`, `cotizaciones.json`, `materiales.json`, `reportes.json` y `usuarios.json`. Cada vez que se agrega o modifica un registro, el sistema lee el archivo completo, hace el cambio en memoria y lo vuelve a escribir entero en disco.

**¿Por qué existe?**
Fue una decisión consciente documentada desde el ADR-02. Se eligió JSON como almacenamiento temporal para poder avanzar en la arquitectura y la lógica del sistema sin depender de tener MySQL configurado. La intención siempre fue migrar a MySQL con Entity Framework Core una vez que el resto del sistema estuviera estable.

**¿Qué pasa si no se resuelve?**
Conforme el sistema crece y se agregan más módulos y registros, los problemas se vuelven más evidentes. No hay relaciones reales entre entidades — si se elimina un cliente, sus siniestros no se eliminan automáticamente. No hay transacciones, no hay control de concurrencia, y si dos usuarios modifican el mismo archivo al mismo tiempo los datos pueden corromperse. Además, cada módulo nuevo que se agrega requiere crear y mantener un nuevo archivo JSON y un nuevo repositorio manual, lo que aumenta la deuda con cada entrega.

**¿Cómo se resolvería?**
La solución está ya definida en el ADR-02: migrar a MySQL usando Entity Framework Core. El patrón Repository implementado en el ADR-05 hace que esta migración sea limpia — solo habría que crear nuevas implementaciones de cada interfaz que usen EF Core en lugar de JSON, y registrarlas en `Program.cs`. Los controladores, servicios y modelos no necesitarían cambiar.

```csharp
// En lugar de:
builder.Services.AddScoped<ISiniestroRepository>(_ => new JsonSiniestroRepository(dataPath));

// Se registraría:
builder.Services.AddScoped<ISiniestroRepository, EfSiniestroRepository>();
```