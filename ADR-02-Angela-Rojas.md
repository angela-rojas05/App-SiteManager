# ADR-02: SiteManager - Implementación de Vistas Arquitectónicas 

| Campo  | Valor |
|--------|-------|
| Autor  | Àngela Rojas |
| Fecha  | 05/06/2026 |
| Estado | `APROBADO` |

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


| Alternativa | Por qué la descarté |
|-------------|---------------------|
|  **Java + Spring Boot**  | Se descartó porque requiere más configuración inicial. Como ahora se usa solo C# y ASP.NET Core en la materia, tiene más sentido trabajar con estas tecnologìas ya que asi el profesor podria brindarme alguna ayuda si la requiero. |
|  **PostgreSQL (Base de datos)**  | Aunque es más potente, su configuración es más compleja. MySQL es más sencillo de usar para un proyecto individual. |
| **Arquitectura de Microservicios** | Es una arquitectura más compleja que no es necesaria para un proyecto pequeño, ademas considerando las otras herramientas que aun quiero dominar me resultaria tedioso. MVC es mas que suficiente. |

---

## Consecuencias

**✅ Lo que gano:**

- **Técnico:** La adopción de MVC con ASP.NET Core me va a permitir una separación clara de responsabilidades desde el inicio. Agregar nuevas entidades al sistema (por ejemplo, una secciòn de registro de pagos o proveedores) implica únicamente crear una nueva clase C#, sin afectar el resto del sistema. Esto hace que el proyecto sea mantenible y mas que nada escalable.

- **Proceso:** Al trabajar sola, Al usar un mismo ecosistema (ASP.NET Core + Entity Framework Core), se reduce la complejidad y el tiempo de configuración. Esto me permite enfocarme más en la lógica del sistema en lugar de en la configuración.

**⚠️ Lo que sacrifico o asumo:**

- **Limitación técnica:** Al usar una arquitectura MVC, todo el backend está en un solo sistema. Si en el futuro se necesita dividirlo en partes más independientes (microservicios), sería necesario hacer cambios grandes.

- **Deuda o riesgo:** Al usar MySQL, se asume que será suficiente para las necesidades del proyecto durante el cuatrimestre. Sin embargo, si el sistema creciera y necesitara manejar más datos o funciones más avanzadas, podría ser necesario cambiar a una base de datos más robusta como PostgreSQL. Hacer este cambio más adelante implicaría modificar migraciones, revisar compatibilidades y ajustar consultas, lo que podría tomar bastante tiempo y esfuerzo.

---

## Diagrama

Un boceto de cómo se estructura tu sistema (draw.io, Mermaid o a mano escaneado)

![Diagrama del sistema](https://www.plantuml.com/plantuml/png/VLJHRjCm57ttLrZTyz3GGAmlQDsjBI7LoqYMy00yt2PMPJMrY-CmTGY9Z-0B-HBSE3LjIfP8pV6zvnpdNDMvRHnQrsvgufIh9SsKam8rhgIbVtxzfZFxhVMbqhKMgIY0a6Qjz1OjRcXfDbkZfNQPstcfzIpKgreUKPPOhbVMjBe2KnjBB_XiV__FXFuWr5ztGyLypXZdeMTi9MrVlxwUJW6wEZU7TrYQkY8_Z-mhTBZ4HewvSyxU0foxSpwTNbsTM6sz5bVZJbVx-znfsbtUYM3fV25lMhJA41BdHIV51_Qf6tUApvxouBTf9lI2tV0td8SDZmRrELDQoSPPKoZvz9LYGxDYo85Q-QUC33XZiE-gahfK63ditCKuzOobClgVKNB6wYD5IbxukCVv8Bb_J9F5WgNXJvKzlBfdZnICbVkOC_wEpLw82KRxtxgMDeI5aR037i1ev06JmNZmeMa28_47n1m6Gn39d2sw_4mxvNQKMzp89sFNXq5mpc_OwnS6qHcBR2H-w8w73dqs7hKtI4O2XdO-x0fqqYgiGriCswdS6djukNG_atWc9sANVLnyqSSVdVdTn3Fz8vvDqczFBo_VqwmuMSBfaWswoUawBmZ9Yk03108282mW1iGhUYMGFC0Sl0Www2SFFVAY7Z4lEXGJalSmZneHKKg8o3euloC28c8HGH9X7f68A3sWMwchTbF_1G00)

---

## Vistas Arquitectónicas

---

**Vista Lógica**

La vista lógica describe los módulos funcionales que componen SiteManager y las responsabilidades de cada uno. El sistema está organizado siguiendo el patrón MVC, donde cada módulo tiene una capa de vista, un controlador y una entidad de modelo correspondiente.

**Módulos funcionales del sistema:**

| Módulo | Responsabilidad |
|---|---|
| **Gestión de Siniestros** | Registrar, actualizar y dar seguimiento a cada caso desde el levantamiento inicial hasta el cierre |
| **Gestión de Clientes** | Administrar la información de los clientes asociados a cada siniestro |
| **Gestión de Evidencias** | Registrar y organizar las fotografías y documentos de respaldo de cada caso |
| **Gestión de Cotizaciones** | Crear y consultar los presupuestos y materiales asociados a cada siniestro |
| **Gestión de Usuarios** | Controlar el acceso al sistema y los permisos según el rol de cada usuario |

Cada módulo se compone de tres elementos dentro de la arquitectura MVC: una vista en Razor Pages que presenta la información al usuario, un controlador en ASP.NET Core que maneja la lógica de negocio, y una entidad de modelo gestionada por Entity Framework Core que representa los datos en la base de datos MySQL.

![Lógica](img/VistaLogica.png)

---


