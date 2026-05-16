# ADR-01: SiteManager - estructura del proyecto 

| Campo  | Valor |
|--------|-------|
| Autor  | Àngela Rojas |
| Fecha  | 15/05/2026 |
| Estado | `Propuesto` |

---

## Contexto

Este proyecto en el desarrollo de una app de gestión de siniestros y levantamientos de obra, por ahora llamada "SiteManager", es una plataforma que tiene como meta facilitar y digitalizar el proceso de registro, seguimiento y administración de levantamientos de daños y reparaciones. Actualmente, este tipo de trabajo se gestiona de forma manual mediante hojas físicas, lo que genera pérdida de información, dificultad para consultar antecedentes de cada caso, desorganización en el manejo de evidencias fotográficas, planos, materiales y presupuestos. Como tal la app busca resolver esto juntando toda la información en una sola plataforma accesible y estructurada.

Está idea va dirigida a profesionales y trabajadores que realizan supervisión de reparaciones: arquitectos, ingenieros, técnicos de mantenimiento y supervisores de obra.

- En cuanto a las condiciones que influyeron en las decisiones de esta idea de proyecto: se tiene experiencia previa en Java, sin embargo, dado que ahora mismo en la materia está trabajando con ASP.NET y C#, se decidió adoptar estas tecnologìas como una oportunidad para expandir el uso de herramientase y mantener una combinacion con los futuros temas que se veran en clase. 
- Para la base de datos se eligió MySQL por su facilidad de configuración en entorno local y porque es mas apta para proyectos de baja magnitud, siendo compatible con Entity Framework Core. 

---

## Decisión

Para empezar escogi la arquitectura de software **Model-View-Controller (MVC)** como patrón estructural principal del sistema. Este patrón separa claramente las responsabilidades: el **Model** representa las entidades del dominio (Siniestro, Cliente, Evidencia, Cotización), el **Controller** maneja la lógica de negocio, y la **View** es solo la parte en la que el cliente o usuario interactua.

**¿Por qué?** El proyecto maneja entidades con relaciones entre sí y procesos (un siniestro pasa por distintas etapas desde el levantamiento hasta el cierre). MVC permite organizar este proceso de forma clara, asignando a cada capa una responsabilidad específica. Esto facilita el mantenimiento y la escalabilidad del proyecto.

---

### ASP.NET Core y C#

Se eligió ASP.NET Core como framework principal del backend por su soporte nativo al patrón MVC.

**¿Por qué?** ASP.NET Core implementa MVC de forma nativa, lo que reduce la configuración manual y permite enfocarse en la lógica del negocio. 

---

### Base de datos: MySQL

Se eligió MySQL como motor de base de datos relacional.

**¿Por qué?** Es una base de datos relacional que permite organizar bien la información del sistema. Es ideal ya que las entidades están relacionadas entre sí (por ejemplo, clientes, siniestros y evidencias), y MySQL ayuda a mantener esa relación de forma ordenada y consistente. También es fácil de usar, instalar y tiene mucha documentación, lo que la hace adecuada para un proyecto pequeño.

---

### Entity Framework

Se usará Entity Framework Core para conectar las clases de C# con la base de datos MySQL.

**¿Por qué?** Permite trabajar las tablas como si fueran clases en el código, lo que hace más fácil mantener todo organizado y consistente. Además, permite hacer cambios en la base de datos de forma controlada mediante migraciones, sin tener que modificarla manualmente cada vez.

---

### Alternativas consideradas

*(Mínimo 3 filas)*

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| ...         | ...                 |
| ...         | ...                 |
| ...         | ...                 |

---

## Consecuencias

**✅ Lo que gano:**

Menciona al menos:
- Una consecuencia **técnica** — qué se vuelve más fácil de construir, mantener o escalar en tu sistema
- Una consecuencia sobre el **proceso o el equipo** — cómo afecta la forma en que vas a trabajar

**⚠️ Lo que sacrifico o asumo:**

Menciona al menos:
- Una **limitación técnica** — qué no podrás hacer fácilmente con esta decisión
- Una **deuda o riesgo** — qué podrías tener que resolver más adelante si el proyecto crece

## Diagrama

Un boceto de cómo se estructura tu sistema (draw.io, Mermaid o a mano escaneado)

![Diagrama del sistema]( ./ruta/diagrama-nivel-1.png )
