# InvoiceBroker

> Middleware asíncrono y tolerante a fallos para la emisión de facturación electrónica a SUNAT (Perú) utilizando UBL 2.1.

## 🚀 Inicio Rápido

El proyecto está diseñado para funcionar de manera autónoma utilizando un contenedor SQL Server efímero para pruebas de integración reales mediante Testcontainers, o usando una base de datos local a través de Docker Compose.

1. Clonar el repositorio.
2. Abrir una terminal en la raíz del proyecto.

### Opción A: Todo en Contenedores (Docker Compose)
3. Levantar la API y la Base de Datos:
   ```bash
   docker compose up --build -d
   ```
4. Navegar a **http://localhost:8080/scalar/v1** o **http://localhost:8080/swagger** para interactuar con la API.

### Opción B: Desarrollo Local (.NET CLI)
3. Levantar solo la base de datos y correr la app manualmente:
   ```bash
   docker compose up -d sqlserver
   dotnet run --project src/InvoiceBroker.Api
   ```
4. Navegar a **http://localhost:5000/scalar/v1** o **http://localhost:5000/swagger**.

## 📦 Arquitectura

El proyecto implementa estrictamente los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**:

- **Dominio:** Entidades y Value Objects puros (inmutabilidad y encapsulamiento estricto). Reglas de negocio agnósticas de infraestructura.
- **Aplicación:** Arquitectura CQRS impulsada por **MediatR**. Validación robusta mediante **FluentValidation** integrada en el pipeline, garantizando que el dominio solo reciba comandos válidos.
- **Infraestructura:** Integración simulada con el servicio SOAP de SUNAT (`billService`). La resiliencia está asegurada mediante **Polly** (Circuit Breaker y Exponential Backoff).
- **API:** Minimal APIs en ASP.NET Core 8 con un `IExceptionHandler` global para la estandarización de respuestas de error.

## ⚙️ Características Técnicas

- Generación estructural de **UBL 2.1** para facturación electrónica.
- Inyección robusta de dependencias usando el patrón Options (`IOptions<T>`) para la configuración de la SUNAT.
- Pruebas de Integración con **Testcontainers** (SQL Server).
- Flujos de Integración y Entrega Continua (CI/CD) usando GitHub Actions (SonarCloud y Semantic Release).

## 📄 Documentación Extendida

- [Arquitectura Detallada (CQRS y Clean Architecture)](docs/architecture.md)
- [Integración con SUNAT y UBL 2.1](docs/sunat_integration.md)
