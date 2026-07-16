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
