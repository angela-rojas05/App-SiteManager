# ADR-01: [Título corto de la decisión]

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

¿Qué decidiste? Sé específico: nombra la tecnología, el patrón o el estilo arquitectónico que elegiste.

### ¿Por qué?

Argumenta tu decisión. No basta con decir "es lo que vimos en clase" — explica qué característica concreta de lo que elegiste resuelve tu problema.

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
