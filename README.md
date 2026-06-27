# 🏗️👷‍♂️ SiteManager

> Aplicación web desarrollada con ASP.NET Core MVC para la gestión de siniestros, levantamientos y reparaciones de obra.

![Estado](https://img.shields.io/badge/Estado-En%20desarrollo-orange)
![Arquitectura](https://img.shields.io/badge/Arquitectura-MVC-blue)
![Framework](https://img.shields.io/badge/.NET-ASP.NET%20Core-purple)
![Licencia](https://img.shields.io/badge/Licencia-Uso%20Académico-green)

---

# Tabla de contenido

- [Descripción](#descripción)
- [Objetivo del proyecto](#objetivo-del-proyecto)
- [Problemática](#problemática)
- [Estado del proyecto](#estado-del-proyecto)
- [Arquitectura del sistema](#arquitectura-del-sistema)
- [Tecnologías utilizadas](#tecnologías-utilizadas)
- [Recursos de desarrollo](#recursos-de-desarrollo)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Descripción de las carpetas](#descripción-de-las-carpetas)
- [Flujo de funcionamiento](#flujo-de-funcionamiento)
- [Módulos y vistas](#módulos-y-vistas)
- [Requisitos](#requisitos)
- [Instalación y ejecución](#instalación-y-ejecución)
- [Capturas de pantalla](#capturas-de-pantalla)
- [Architectural Decision Record (ADR)](#architectural-decision-record-adr)
- [Próximas mejoras](#próximas-mejoras)
- [Información del proyecto](#información-del-proyecto)

---

# Descripción

SiteManager es una aplicación web desarrollada como proyecto académico con el objetivo de digitalizar la administración de siniestros y levantamientos de obra.

La aplicación busca centralizar toda la información relacionada con un caso, permitiendo registrar clientes, documentar evidencias, administrar cotizaciones y dar seguimiento al estado de cada siniestro desde su creación hasta su cierre.

Esta rama corresponde a la primera versión del proyecto, donde se establece la estructura inicial utilizando el patrón arquitectónico **Modelo–Vista–Controlador (MVC)**.

---

# Objetivo del proyecto

Desarrollar una plataforma web que facilite el registro y seguimiento de siniestros de manera organizada, reduciendo el uso de documentos físicos y mejorando el acceso a la información durante todo el proceso de reparación.

---

# Problemática

Actualmente muchos levantamientos de obra continúan administrándose mediante hojas físicas, fotografías almacenadas en diferentes dispositivos y archivos dispersos.

Esta forma de trabajo puede provocar:

- Pérdida de información.
- Duplicidad de registros.
- Dificultad para consultar antecedentes.
- Desorganización de evidencias.
- Seguimiento poco eficiente del avance de cada caso.

SiteManager surge como una alternativa para concentrar toda esta información dentro de una sola aplicación.

---

# Estado del proyecto

🚧 **Versión inicial**

En esta etapa se definió la arquitectura base del sistema utilizando ASP.NET Core MVC.

Como almacenamiento temporal de la información se emplean archivos **JSON**, permitiendo desarrollar la lógica del sistema antes de integrar una base de datos relacional.

---

# Arquitectura del sistema

La primera decisión arquitectónica del proyecto fue utilizar el patrón **Modelo–Vista–Controlador (MVC)**.

Este patrón divide la aplicación en tres componentes principales:

- **Model:** representa las entidades del negocio.
- **View:** muestra la información al usuario.
- **Controller:** procesa las solicitudes y coordina la comunicación entre modelos y vistas.

Esta separación facilita el mantenimiento del código y permite que cada componente tenga una responsabilidad específica.

```text
Usuario
    │
    ▼
 Views
    │
    ▼
Controllers
    │
    ▼
 Models
    │
    ▼
 Archivos JSON
```

---

# Tecnologías utilizadas

| Tecnología | Uso |
|------------|-----|
| ASP.NET Core MVC | Framework principal para el desarrollo web. |
| C# | Implementación de la lógica del sistema. |
| Razor Views | Construcción de las vistas dinámicas. |
| HTML5 | Estructura de las páginas web. |
| CSS3 | Diseño y estilos de la interfaz. |
| Bootstrap 5 | Diseño responsivo y componentes visuales. |
| JavaScript | Interacciones y validaciones del lado del cliente. |
| JSON | Persistencia temporal de la información. |
| Git | Control de versiones. |
| GitHub | Administración del repositorio. |

---

# Recursos de desarrollo

Durante esta etapa se utilizaron las siguientes herramientas:

- Visual Studio 2022
- .NET SDK
- Git
- GitHub
- Bootstrap
- Archivos JSON como almacenamiento temporal

---

# Estructura del proyecto

Durante esta primera versión, SiteManager se encuentra organizado como una aplicación ASP.NET Core MVC, concentrando la interfaz, la lógica de la aplicación y el almacenamiento temporal dentro de un mismo proyecto.

```text
SiteManager

├── Controllers/
├── Models/
├── Views/
├── Data/
├── AppData/
├── wwwroot/
├── Properties/
├── Program.cs
├── appsettings.json
└── SiteManager.Web.csproj
```

## Descripción de las carpetas

| Carpeta | Descripción |
|----------|-------------|
| **Controllers** | Contiene los controladores encargados de atender las solicitudes del usuario y coordinar la lógica de cada módulo del sistema. |
| **Models** | Incluye las entidades que representan la información del negocio, como siniestros, clientes, materiales y demás registros utilizados por la aplicación. |
| **Views** | Contiene las vistas desarrolladas con Razor, responsables de mostrar la información y permitir la interacción con el usuario. |
| **Data** | Almacena los archivos JSON utilizados como persistencia temporal y la configuración relacionada con el acceso a los datos durante esta etapa del proyecto. |
| **AppData** | Reúne archivos auxiliares empleados por la aplicación para almacenar información temporal de los diferentes módulos. |
| **wwwroot** | Contiene los recursos estáticos de la aplicación, como hojas de estilo, archivos JavaScript, imágenes e íconos. |
| **Properties** | Incluye la configuración propia del proyecto para su ejecución dentro del entorno de desarrollo. |

---

# Flujo de funcionamiento

El flujo básico del sistema sigue el patrón MVC.

1. El usuario realiza una acción desde la interfaz.
2. El controlador recibe la petición.
3. Se procesa la lógica correspondiente.
4. La información se consulta o almacena en archivos JSON.
5. Finalmente se devuelve una vista actualizada al usuario.

---

# Módulos y vistas

La aplicación está organizada en distintos módulos que permiten administrar la información relacionada con un siniestro durante todo su ciclo de vida.

| Módulo | Descripción |
|---------|-------------|
| **Inicio** | Página principal desde la cual el usuario accede a las funcionalidades del sistema. |
| **Siniestros** | Permite registrar, consultar, editar y eliminar los diferentes siniestros administrados por la aplicación. |
| **Clientes** | Gestiona la información de los clientes asociados a cada siniestro. |
| **Evidencias** | Administra las fotografías y archivos de respaldo que documentan cada caso. |
| **Cotizaciones** | Permite registrar y consultar los presupuestos relacionados con las reparaciones. |
| **Materiales** | Controla los materiales requeridos para la ejecución de los trabajos de reparación. |
| **Reportes** | Genera consultas y reportes para facilitar el seguimiento de la información registrada. |
| **Usuarios** | Administra los usuarios que interactúan con la aplicación y la información asociada a ellos. |

Cada módulo sigue la estructura definida por el patrón MVC, contando con su correspondiente controlador, modelo y vista para mantener una separación clara de responsabilidades.

---

# Requisitos

- .NET SDK
- Visual Studio 2022
- Git

---

# Instalación y ejecución

Clonar el repositorio

```bash
git clone https://github.com/angela-rojas05/App-SiteManager.git
```

Entrar al proyecto

```bash
cd SiteManager
```

Restaurar dependencias

```bash
dotnet restore
```

Compilar

```bash
dotnet build
```

Ejecutar

```bash
dotnet run
```

---

# Capturas de pantalla

> Aquí se agregarán capturas representativas de la aplicación.

- Página de inicio.
- Gestión de siniestros.
- Registro de clientes.
- Gestión de evidencias.
- Cotizaciones.

---

# Architectural Decision Record (ADR)

Durante esta etapa se documentó la primera decisión arquitectónica del proyecto.

## ADR-01 — Estructura inicial del proyecto

La primera decisión consistió en adoptar el patrón **Modelo–Vista–Controlador (MVC)** como base para organizar la aplicación.

Esta decisión permitió establecer una separación clara entre la interfaz de usuario, la lógica de negocio y los modelos de datos, facilitando el mantenimiento del proyecto y preparando una base sólida para futuras evoluciones arquitectónicas.

📄 **Documento:** `ADR-01`

---

# Próximas mejoras

Las siguientes etapas del proyecto contemplan:

- Reorganización de la arquitectura.
- Separación de responsabilidades.
- Incorporación de nuevos componentes.
- Exposición de servicios mediante API.
- Implementación de patrones de diseño.

---

## 🤖 Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizó inteligencia artificial (ChatGPT de OpenAI) como herramienta de apoyo en tareas específicas, entre ellas:

- Apoyo en la resolución de errores (debugging) y análisis de problemas durante el desarrollo.
- Asistencia en la reorganización del proyecto al migrar hacia una arquitectura por capas, ayudando a identificar posibles conflictos y alternativas de solución.
- Sugerencias para mejorar la organización y navegación de la interfaz de usuario, buscando una experiencia más clara e intuitiva.
- Recomendaciones para el diseño visual de la aplicación, incluyendo estilos, distribución de elementos, paleta de colores y mejoras en la presentación de las vistas.
- Apoyo en la redacción y organización de la documentación técnica del proyecto, incluyendo los archivos README y los documentos ADR.

La implementación del código, la toma de decisiones arquitectónicas, las pruebas y la integración final de las funcionalidades fueron realizadas por la autora del proyecto.

---
# Información del proyecto

**Proyecto:** SiteManager

**Desarrollado por:** Ángela Rojas

**Materia:** Arquitectura de Software

**Repositorio:** *https://github.com/angela-rojas05/App-SiteManager.git*

**Licencia:** Uso académico.
