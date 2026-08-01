# ADR-08: SiteManager — Migración a PostgreSQL e Implementación de Persistencia Real

| Campo  | Valor |
|--------|-------|
| Autor  | Ángela Rojas |
| Fecha  | 31/07/2026 |
| Estado | `APROBADO` |

---

## Contexto

Desde el inicio del proyecto, SiteManager utilizó archivos JSON como mecanismo de almacenamiento temporal. Esta decisión fue documentada en el ADR-02 como una deuda técnica consciente, con la intención de migrar a una base de datos relacional una vez que la arquitectura del sistema estuviera estable.

En el ADR-02 se había elegido MySQL como motor de base de datos. Sin embargo, al evaluar las necesidades reales del sistema, se identificó que SiteManager maneja un módulo de Evidencias que requiere almacenar archivos de imagen asociados a cada siniestro. Esto, junto con la experiencia previa que ya se tiene con PostgreSQL, llevó a reconsiderar la decisión original.

---

## Decisión

Se decide migrar el almacenamiento de SiteManager de archivos JSON a **PostgreSQL** como motor de base de datos relacional, usando **Entity Framework Core** con el proveedor `Npgsql` para la conexión.

Para el manejo de imágenes en el módulo de Evidencias, se adopta la estrategia de **almacenamiento en servidor**: las imágenes se guardan como archivos físicos en una carpeta `uploads/` dentro del proyecto, y en la base de datos se guarda únicamente la ruta relativa del archivo. Esto mantiene la base de datos ligera y permite acceder a las imágenes directamente desde el navegador.

---

## ¿Por qué PostgreSQL y no MySQL?

| Criterio | MySQL | PostgreSQL |
|---|---|---|
| Experiencia previa del estudiante | Limitada | Alta |
| Soporte en EF Core | Sí (Pomelo) | Sí (Npgsql, oficial) |
| Soporte para tipos de datos avanzados | Básico | Superior |
| Documentación y comunidad | Amplia | Amplia |
| Configuración local | Similar | Ya instalado |

La principal razón del cambio es práctica: PostgreSQL ya está instalado en el entorno de desarrollo y es la base de datos con la que se tiene mayor experiencia, lo que reduce el riesgo de errores de configuración y acelera la implementación.

---

## Estrategia para el manejo de imágenes

El módulo de Evidencias permite adjuntar archivos de imagen como respaldo fotográfico de los daños de un siniestro. La estrategia elegida es:

- El usuario sube una imagen desde el formulario de Evidencias.
- El servidor recibe el archivo y lo guarda en la carpeta `wwwroot/uploads/evidencias/`.
- En la base de datos, el campo `RutaArchivo` de la entidad `Evidencia` guarda la ruta relativa del archivo (`/uploads/evidencias/nombre-archivo.jpg`).
- Las vistas muestran la imagen usando esa ruta directamente como `src` de una etiqueta `<img>`.

Esta estrategia es simple, no requiere servicios externos y es adecuada para el tamaño actual del proyecto.

---

## Alternativas consideradas

| Alternativa | Por qué se descartó |
|---|---|
| **MySQL** | Decisión original del ADR-02. Se cambió por la experiencia previa con PostgreSQL y porque el proveedor Npgsql tiene soporte oficial de Microsoft, a diferencia de Pomelo que es de la comunidad. |
| **Almacenar imágenes en la base de datos (BYTEA)** | PostgreSQL permite guardar archivos binarios directamente en la base de datos. Se descartó porque aumenta considerablemente el tamaño de la base de datos y hace las consultas más lentas. |
| **Almacenamiento en la nube (S3, Cloudinary)** | Es la solución más profesional para producción, pero requiere configurar servicios externos de pago que están fuera del alcance del cuatrimestre. |

---

## Consecuencias

**✅ Lo que gano:**

- **Persistencia real:** Los datos ya no dependen de archivos JSON que pueden corromperse o perderse. PostgreSQL garantiza integridad referencial entre entidades.
- **Relaciones reales:** Si se elimina un cliente, sus siniestros pueden eliminarse en cascada automáticamente. Esto resuelve la Deuda 3 documentada en el ADR-06.
- **Imágenes funcionales:** El módulo de Evidencias pasa de ser un campo decorativo a una funcionalidad real que permite subir y visualizar fotografías de los daños.
- **Migraciones controladas:** EF Core gestiona los cambios en el esquema de la base de datos de forma ordenada mediante migraciones, sin necesidad de modificar la base de datos manualmente.

**⚠️ Lo que sacrifico o asumo:**

- **Dependencia de PostgreSQL:** El sistema ya no puede correr sin tener PostgreSQL instalado y configurado. Esto aumenta los requisitos de instalación.
- **Imágenes en servidor local:** Las imágenes se guardan en el servidor donde corre la aplicación. Si el servidor cambia, las imágenes deben migrarse manualmente junto con la base de datos.
- **Sin respaldo automático de imágenes:** A diferencia de un servicio en la nube, las imágenes en el servidor local no tienen respaldo automático.

---

## Cláusula de IA

Para la elaboración de este documento se utilizó inteligencia artificial (Claude, de Anthropic) como herramienta de apoyo en las siguientes tareas:

- Evaluación de alternativas para el motor de base de datos y la estrategia de manejo de imágenes
- Estructuración del ADR-08
- Argumentación de las decisiones tomadas con base en el contexto real del proyecto

Todo el contenido fue revisado y validado para asegurar que refleja correctamente las decisiones y el contexto real de SiteManager.