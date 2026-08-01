# Modelo C4 — SiteManager

---

## Nivel 1 — Contexto
 
- **¿Para quién es este diagrama?** Para stakeholders no técnicos como clientes o directivos que necesitan entender el sistema sin entrar en detalles técnicos.

- **¿Qué es el sistema?** Una vista de alto nivel que responde quién interactúa con SiteManager y qué resuelve. En este nivel no importa cómo está construido por dentro, solo qué hace y para quién existe.


```mermaid
C4Context
    title Diagrama de Contexto — SiteManager

    Person(usuario, "Usuario Interno", "Técnico, supervisor, arquitecto o ingeniero que gestiona siniestros, evidencias, cotizaciones y reparaciones desde el navegador.")

    System(sitemanager, "SiteManager", "Plataforma web para gestionar siniestros, levantamientos, evidencias, cotizaciones y reparaciones de obra.")

    Person(sistemaExterno, "Sistema Externo", "Consume la API REST para obtener o modificar información de siniestros.")

    Rel(usuario, sitemanager, "Usa", "HTTPS / Navegador")
    Rel(sistemaExterno, sitemanager, "Consume API REST", "HTTPS / JSON")
```