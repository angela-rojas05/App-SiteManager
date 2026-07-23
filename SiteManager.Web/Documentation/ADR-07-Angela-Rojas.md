# ADR-07: SiteManager — Suite de Pruebas y Pipeline CI

| Campo  | Valor |
|--------|-------|
| Autor  | Ángela Rojas |
| Fecha  | 23/07/2026 |
| Estado | `APROBADO` |

---

## Contexto

Conforme SiteManager evolucionó a través de los ADRs anteriores, la lógica del sistema fue creciendo en complejidad. Sin un mecanismo formal de verificación, cualquier cambio en el código podría romper funcionalidades existentes sin que nadie lo notara hasta que el error apareciera en producción.

Para garantizar que el sistema se mantiene funcional en cada cambio, se decidió agregar una suite de pruebas automatizadas y un pipeline de Integración Continua que las ejecute automáticamente en cada push al repositorio.

---

## Decisión

Se implementó una suite de pruebas con **xUnit** dentro de un proyecto dedicado llamado `SiteManager.xUnit`, y se configuró un workflow de **GitHub Actions** que compila la solución y ejecuta todas las pruebas automáticamente en cada push a cualquier rama.

---

## Clases probadas

Se eligieron tres clases de capas distintas para cubrir la arquitectura del sistema de forma representativa.

### `ClienteService` — Capa Application

**¿Por qué se eligió?**
Es el servicio más representativo del patrón Repository. Verifica que las operaciones básicas de agregar, obtener, actualizar y eliminar clientes funcionan correctamente a través de la interfaz, sin depender de archivos ni base de datos real.

**Pruebas implementadas:**
- `Agregar_ClienteNuevo_SeGuardaCorrectamente`
- `ObtenerPorId_ClienteExiste_RetornaCliente`
- `ObtenerPorId_ClienteNoExiste_RetornaNull`
- `Eliminar_ClienteExistente_YaNoEstaEnLaLista`
- `Actualizar_ClienteExistente_CambiaElNombre`

---

### `SiniestroService` — Capa Application

**¿Por qué se eligió?**
Es la clase que implementa el patrón Observer definido en el ADR-05. Se eligió para verificar que al actualizar un siniestro, los observers son notificados correctamente, y que las operaciones básicas del servicio funcionan como se espera.

**Pruebas implementadas:**
- `Agregar_SiniestroNuevo_SeGuardaCorrectamente`
- `ObtenerTodos_ConSiniestros_RetornaLista`
- `Actualizar_Siniestro_NotificaAlObserver`
- `Eliminar_SiniestroExistente_YaNoEstaEnLaLista`

---

### `JsonSiniestroRepository` — Capa Infrastructure

**¿Por qué se eligió?**
Es la implementación concreta del repositorio que trabaja directamente con archivos JSON en disco. Se eligió para probar la capa de Infrastructure con archivos temporales reales que se crean y eliminan en cada prueba, verificando que la persistencia funciona correctamente. Esta clase está directamente relacionada con la Deuda 1 y la Deuda 3 documentadas en el ADR-06.

**Pruebas implementadas:**
- `Agregar_Siniestro_SeGuardaEnArchivo`
- `ObtenerPorId_SiniestroExiste_RetornaCorrecto`
- `Eliminar_Siniestro_YaNoExisteEnArchivo`

---

## Pipeline de Integración Continua

Se configuró un workflow de GitHub Actions en `.github/workflows/ci.yml` que se ejecuta automáticamente en cada push a cualquier rama. El pipeline realiza tres pasos: restaurar dependencias, compilar la solución y ejecutar todas las pruebas.

---

## Consecuencias

**✅ Lo que gano:**

- **Confianza en los cambios:** Cada push al repositorio ejecuta las 12 pruebas automáticamente. Si algo se rompe, el pipeline lo detecta antes de que llegue a producción.
- **Cobertura de las capas principales:** Las pruebas cubren Application e Infrastructure, las dos capas donde vive la lógica del sistema.
- **Verificación del patrón Observer:** Las pruebas de `SiniestroService` confirman que el patrón implementado en el ADR-05 funciona correctamente.

**⚠️ Lo que sacrifico o asumo:**

- **Cobertura parcial:** Solo se probaron 3 clases de las más representativas. Los demás servicios y repositorios no tienen pruebas todavía.
- **Sin pruebas de controllers ni vistas:** La capa de Presentation no está cubierta por las pruebas actuales.

---

## Cláusula de IA

Para la elaboración de este documento se utilizó inteligencia artificial (Claude, de Anthropic) como herramienta de apoyo en las siguientes tareas:

- Diseño de la suite de pruebas y selección de las clases a probar
- Implementación de los repositorios y observers fake para las pruebas
- Configuración del workflow de GitHub Actions

Todo el contenido fue revisado y validado por la autora para asegurar que refleja correctamente las decisiones y el contexto real de SiteManager.