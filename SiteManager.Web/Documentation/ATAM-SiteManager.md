# Evaluación ATAM — SiteManager

| Campo | Valor |
|---|---|
| Autora | Ángela Rojas |
| Fecha | 31/08/2026 |
| Proyecto | SiteManager |

---

## Introducción

Este documento presenta una evaluación arquitectónica de SiteManager utilizando
el método **ATAM (Architecture Tradeoff Analysis Method)**. El objetivo es
identificar un riesgo, un trade-off y un punto de sensibilidad reales dentro
de la arquitectura del sistema, cada uno justificado con una decisión
documentada en los ADR del proyecto.

---

## 1. Riesgo

### Escenario

Cuando un siniestro cambia de estado (por ejemplo, de "En proceso" a
"Cerrado"), el sistema debe notificar a los usuarios involucrados
(técnicos, supervisores) para que estén al tanto del avance del caso.

### Riesgo identificado

El patrón **Observer**, implementado en el **ADR-05** para desacoplar la
lógica de notificación de la clase `Siniestro`, está correctamente
estructurado a nivel de diseño (`ISiniestroObserver`, `EmailObserver`,
`Siniestro` manteniendo una lista de observers). Sin embargo, la
implementación concreta de `EmailObserver` **no envía ninguna notificación
real** — únicamente imprime un mensaje en la consola del servidor:

```csharp
Console.WriteLine($"[Email] Notificación enviada — Siniestro #{siniestro.Id}: {siniestro.TipoDanio} cambió a estado {siniestro.Estado}.");
```

Esta limitación está documentada explícitamente como **Deuda Técnica 2**
en el **ADR-06**.

### Atributo de calidad afectado

**Observabilidad del negocio / Disponibilidad de información.** El riesgo
es que, en un entorno de uso real, ningún técnico o supervisor se entera
de un cambio de estado, ya que la "notificación" nunca sale del servidor.
El patrón está bien construido internamente, pero no cumple su propósito
funcional frente al usuario final.

### Justificación con decisión real de la arquitectura

- **ADR-05:** decisión de implementar el patrón Observer para las
  notificaciones de cambio de estado.
- **ADR-06:** reconoce formalmente esta limitación como deuda técnica,
  documentando que la solución (conectar un servicio real de correo como
  SendGrid o SMTP) solo requeriría modificar `EmailObserver`, sin tocar
  controladores ni servicios — gracias a que el patrón ya desacopla esa
  responsabilidad.

---

## 2. Trade-off

### Escenario

El módulo de Evidencias necesita almacenar imágenes (fotografías de daños)
asociadas a cada siniestro, como parte de la migración a PostgreSQL
documentada en el **ADR-08**.

### Trade-off identificado

Se decidió almacenar las imágenes como **archivos físicos en el servidor**
(`wwwroot/uploads/evidencias/`), guardando en PostgreSQL únicamente la
ruta relativa del archivo (`RutaArchivo`), en lugar de:

- Guardar la imagen directamente en la base de datos como `BYTEA`, o
- Subirla a un servicio de almacenamiento en la nube (S3, Cloudinary, etc.)

Este trade-off enfrenta dos atributos de calidad:

| Se gana | Se sacrifica |
|---|---|
| **Performance:** la base de datos permanece ligera; las imágenes se sirven directamente como archivos estáticos, sin sobrecargar las consultas SQL. | **Disponibilidad / Escalabilidad:** si el servidor cambia de máquina, las imágenes deben migrarse manualmente junto con la base de datos. No hay respaldo automático ni replicación, como sí tendría un servicio en la nube. |
| **Simplicidad de implementación:** no requiere configurar credenciales ni SDKs de terceros, adecuado para el alcance de un proyecto de un cuatrimestre. | **Recuperación ante desastres:** si se pierde el disco del servidor, se pierden las imágenes aunque la base de datos esté respaldada aparte. |

### Justificación con decisión real de la arquitectura

Esta decisión y sus alternativas descartadas están documentadas
explícitamente en la sección de alternativas del **ADR-08**, donde se
evalúa y descarta tanto el almacenamiento en `BYTEA` como el
almacenamiento en la nube, priorizando simplicidad y tiempo de desarrollo
sobre escalabilidad a futuro.

---

## 3. Punto de Sensibilidad

### Escenario

El sistema necesita poder cambiar su mecanismo de persistencia (de dónde y
cómo se guardan los datos) sin tener que reescribir la lógica de negocio
ni los controladores.

### Punto de sensibilidad identificado

La **Modificabilidad** de SiteManager es altamente sensible a qué tan bien
esté aplicada la abstracción del patrón **Repository**, introducido en el
**ADR-05**.

Esto se puede demostrar comparando dos momentos reales y verificables del
proyecto:

1. **ADR-05:** se introducen las interfaces `IClienteRepository`,
   `ISiniestroRepository`, `IEvidenciaRepository`, etc., de forma que la
   capa Application depende de abstracciones y no de una implementación
   concreta de acceso a datos.
2. **ADR-08:** al migrar de JSON a PostgreSQL con Entity Framework Core,
   el único cambio necesario fue el registro de dependencias en
   `Program.cs`:

```csharp
// Antes:
builder.Services.AddScoped<IClienteRepository, JsonClienteRepository>();

// Después:
builder.Services.AddScoped<IClienteRepository, EfClienteRepository>();
```

Ningún controlador, ningún servicio de Application ni ningún modelo de
Domain tuvo que modificarse para completar esta migración.

### Justificación con decisión real de la arquitectura

Si esta abstracción no se hubiera respetado de forma consistente en todo
el código — por ejemplo, si algún controlador hubiera llamado directamente
a `JsonSiniestroRepository` en lugar de a la interfaz `ISiniestroRepository`
— la migración documentada en el ADR-08 habría sido mucho más costosa y
riesgosa, requiriendo tocar múltiples capas simultáneamente.

Esto confirma que el logro del atributo de calidad de **Modificabilidad**
en SiteManager depende sensiblemente de una única decisión de diseño: la
disciplina de acceder a los datos siempre a través de las interfaces de
Repository definidas en Domain, nunca de forma directa.

---

## Conclusión

| Elemento | Decisión de arquitectura que lo origina | Atributo de calidad relacionado |
|---|---|---|
| Riesgo | ADR-05 (Observer) + ADR-06 (deuda reconocida) | Observabilidad del negocio |
| Trade-off | ADR-08 (almacenamiento de evidencias) | Performance vs. Disponibilidad/Escalabilidad |
| Sensibilidad | ADR-05 (Repository) validado por ADR-08 (migración) | Modificabilidad |

---

## Cláusula de IA

Para la elaboración de este documento se utilizó inteligencia artificial
(Claude, de Anthropic) como herramienta de apoyo en las siguientes tareas:

- Estructuración del documento siguiendo el formato del método ATAM.
- Redacción y organización del contenido.

Todo el contenido fue revisado y validado por la autora para asegurar que
refleja correctamente las decisiones y el contexto real de SiteManager.