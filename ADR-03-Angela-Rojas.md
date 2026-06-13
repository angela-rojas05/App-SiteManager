# ADR-03: SiteManager — Estilo Arquitectónico

| Campo  | Valor |
|--------|-------|
| Autor  | Ángela Rojas |
| Fecha  | 12/06/2026 |
| Estado | `APROBADO` |

---

## Contexto

SiteManager es una aplicación web que busca digitalizar la gestión de siniestros, levantamientos y reparaciones de obra. El sistema maneja varias entidades que se relacionan entre sí, como Siniestro, Cliente, Evidencia y Cotización, además de flujos de trabajo definidos que van desde el registro de un caso hasta su cierre.

Al ser un proyecto individual con un tiempo limitado a la duración del cuatrimestre, se necesita un estilo arquitectónico que sea claro, fácil de mantener por una sola persona y que sea compatible con las tecnologías ya elegidas: ASP.NET Core, Razor Pages, Entity Framework Core y MySQL.

---

## Decisión


Se eligió la **Arquitectura en Capas (Layered Architecture)** como estilo arquitectónico de SiteManager. El sistema se organiza en cuatro capas bien definidas, donde cada una tiene una responsabilidad específica y solo depende de la capa que tiene debajo:

| Capa | Responsabilidad | En SiteManager |
|---|---|---|
| **Presentation** | Presentar la información al usuario y recibir sus acciones | Razor Pages |
| **Application** | Manejar la lógica de negocio y coordinar las operaciones | Controladores ASP.NET Core |
| **Domain** | Definir las entidades y reglas del negocio | Modelos C# (Siniestro, Cliente, Evidencia, Cotización, etc.) |
| **Infrastructure** | Gestionar el acceso a datos y servicios externos | Entity Framework Core + MySQL |

**¿Por qué?** SiteManager ya tiene estas cuatro capas de forma natural en su estructura. Razor Pages maneja la interfaz, los controladores toman las decisiones, los modelos definen qué es cada entidad y Entity Framework Core se encarga de persistir todo en MySQL. Documentar esto como Arquitectura en Capas no es forzar una decisión, sino reconocer y formalizar la estructura que el sistema ya tiene. Además, este estilo garantiza que cada capa pueda modificarse sin afectar a las demás, lo cual es crítico cuando se desarrolla en solitario y los cambios deben ser controlados y predecibles.

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
|---|---|
| **Microservicios** | Requiere dividir el sistema en servicios completamente independientes, cada uno con su propia base de datos, despliegue y comunicación entre sí. SiteManager maneja entidades muy relacionadas entre sí, como Siniestros, Clientes y Evidencias, por lo que separarlas en servicios distintos complicaría innecesariamente algo que funciona mejor unido. Además, al ser un proyecto individual que corre en entorno local, mantener múltiples servicios corriendo al mismo tiempo sería difícil de gestionar. |
| **Arquitectura Hexagonal** | Es un estilo muy limpio que separa la lógica de negocio de todo lo externo, como la base de datos o la interfaz. Sin embargo, para aplicarlo correctamente se necesitan interfaces, adaptadores y puertos que agregan capas de abstracción que SiteManager no necesita en este momento. La Arquitectura en Capas ya logra esa separación de forma más directa y sin tanta configuración adicional. |
| **Event-Driven** | Este estilo funciona cuando el sistema necesita reaccionar a muchos eventos de forma desacoplada y en tiempo real. SiteManager tiene flujos de trabajo lineales y bien definidos: el usuario registra un siniestro, el controlador lo procesa y EF Core lo guarda en MySQL. No hay necesidad de un sistema de eventos para coordinar eso, ya que el flujo es predecible y directo. |
| **Serverless** | Implica dividir toda la lógica en funciones independientes desplegadas en la nube. SiteManager actualmente corre en entorno local con ASP.NET Core como un solo proyecto, y toda su estructura, desde los controladores hasta el contexto de Entity Framework Core, está pensada para funcionar como una aplicación unificada. Migrar a serverless implicaría reescribir gran parte de lo ya definido en el ADR-01 y ADR-02. | por una sola persona, este estilo no encaja con la realidad del proyecto. |

---

## Consecuencias

**✅ Lo que gano:**

- **Técnico:** La separación en capas hace que cada parte del sistema tenga una responsabilidad clara. Si necesito cambiar cómo se ve una pantalla, solo toco Razor Pages sin afectar la lógica de negocio. Si cambio cómo se guarda un siniestro en la base de datos, solo toco la capa de Infrastructure sin que las demás capas se enteren. Eso hace que el código sea más fácil de mantener y modificar conforme el proyecto avanza.

- **Proceso:** Al trabajar sola, tener capas bien definidas me permite concentrarme en una parte del sistema a la vez sin perder el hilo de lo que hace cada cosa. La estructura es predecible: siempre sé que la lógica vive en los controladores, las entidades en los modelos y el acceso a datos en Entity Framework Core.

- **Coherencia:** Este estilo es completamente compatible con las decisiones tomadas en el ADR-01 y ADR-02. No requiere cambiar el stack tecnológico ni la organización del proyecto, sino que formaliza y documenta la estructura que SiteManager ya tiene de forma natural.

**⚠️ Lo que sacrifico o asumo:**

- **Limitación técnica:** En la Arquitectura en Capas, las capas superiores dependen de las inferiores. Si en algún momento se quisiera cambiar MySQL por otro motor de base de datos, ese cambio afectaría la capa de Infrastructure y potencialmente la de Domain, lo que requeriría revisar las migraciones y el contexto de Entity Framework Core.

- **Escalabilidad limitada:** Este estilo funciona muy bien para el tamaño actual de SiteManager, pero si el sistema creciera significativamente en funcionalidades o usuarios concurrentes, la Arquitectura en Capas podría volverse un cuello de botella. En ese escenario, migrar a un estilo como microservicios sería el siguiente paso natural.

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

## Vista de Desarrollo

---

La vista de desarrollo muestra cómo está organizado el código del proyecto por dentro. SiteManager sigue la estructura estándar de ASP.NET Core con MVC, donde cada carpeta tiene una responsabilidad clara y separada.


![Desarrollo](img/VistaDesarrollo.png)


-Controllers/ — Aquí viven los controladores de cada módulo. Cada archivo recibe las peticiones del usuario, aplica la lógica de negocio y decide qué datos mostrar y en qué vista.

-Models/ — Aquí están las clases C# que representan las entidades del sistema. Cada clase se convierte en una tabla dentro de la base de datos MySQL a través de Entity Framework Core.

-Views/ — Aquí están las vistas Razor organizadas por módulo. Cada subcarpeta contiene los archivos de interfaz correspondientes a su controlador; por ejemplo, la carpeta Siniestro contiene las páginas para crear, editar, listar y ver el detalle de un siniestro.

-Data/ — Aquí vive el contexto de Entity Framework Core (SiteManagerContext), que es la clase que conecta el proyecto con la base de datos MySQL y registra todas las entidades que se van a persistir.

---

## Vista de Procesos

--- 

La vista de procesos muestra el flujo que sigue una operación importante dentro del sistema. En SiteManager, el proceso más relevante es el **registro de un siniestro nuevo**, ya que es el punto de entrada de toda la información que el sistema gestiona.

![Proceso](img/VistaProceso.png)


1. Petición: El usuario registra un siniestro desde el navegador, enviando una petición HTTP al sistema.
2. Procesamiento Sincrónico: ASP.NET Core MVC recibe los datos. El controlador los valida y le ordena al modelo guardarlos. El sistema espera la confirmación antes de continuar.
3. Persistencia: Entity Framework Core guarda el registro en la base de datos MySQL de forma segura.
4. Evento Asíncrono: Con el registro confirmado, la aplicación web avisa al Servicio de Notificaciones. Al ser asíncrono, la web no se bloquea esperando que el correo se envíe; el usuario ya recibió su respuesta.
5. Envío: El Servicio de Notificaciones procesa la cola en segundo plano y envía el correo electrónico al técnico asignado.

---

## Vista de Despliegue

---

La vista de despliegue describe dónde y cómo se planea ejecutar el sistema. Dado que SiteManager se encuentra actualmente en fase de desarrollo, el despliegue planeado para esta etapa es en entorno local, utilizando la máquina de desarrollo como servidor.

![Despliegue](img/VistaDespliegue.png)

Por ahora SiteManager corre en la computadora local. El usuario abre un navegador, entra a localhost y el sistema responde desde ahí. La aplicación y la base de datos MySQL viven en la misma máquina y se comunican entre sí.

---

## Cláusula de IA 

Se utilizó inteligencia artificial como herramienta de apoyo en las siguientes tareas:

- Generación de los códigos Mermaid para los diagramas de cada vista arquitectónica (lógica, desarrollo, procesos y despliegue)
- Estructuración del ADR-02 siguiendo el formato establecido.
- Sugerencias sobre qué incluir en cada vista con base en el proyecto SiteManager.


