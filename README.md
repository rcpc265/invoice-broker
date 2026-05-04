# InvoiceBroker (Mi primer proyecto .NET)

> Middleware asíncrono para emisión de facturación electrónica a SUNAT (Perú) con UBL 2.1.
> *Estado: En Progreso (Migrando de InMemory a SQL Server con Testcontainers)*

## 🚀 Inicio Rápido 

Por ahora, el proyecto utiliza una base de datos en memoria para facilitar las pruebas.

1. Clonar el repositorio.
2. Abrir una terminal en la raíz del proyecto.
3. Ejecutar:
   ```bash
   dotnet run --project src/InvoiceBroker.Api
   ```
4. Navegar a **http://localhost:5000/scalar/v1** o **http://localhost:5000/swagger** para ver el panel interactivo de la API.

*(Nota: Estoy trabajando en la configuración de Docker Compose para levantar la base de datos SQL Server real).*

## 📦 Arquitectura (Lo que he aprendido hasta ahora)

He estructurado este proyecto basándome en tutoriales de **Clean Architecture** y principios **DDD**. Hasta el momento he logrado implementar:
- **CQRS con MediatR:** Separación de comandos y consultas.
- **Validaciones:** Uso de `FluentValidation` en el pipeline de MediatR para asegurar que los datos del comprobante sean correctos antes de llegar al dominio.
- **Resiliencia:** Integración básica con `Polly` para manejar reintentos y circuit breakers al comunicarse con SUNAT.
- **Generación XML (UBL 2.1):** Un simulador que genera la estructura XML requerida por SUNAT.

## 🛠️ Próximos Pasos (Mi Roadmap)
- [ ] Configurar Testcontainers para pruebas de integración reales.
- [ ] Conectar la base de datos EF Core a SQL Server mediante Docker.
- [ ] Implementar la firma digital criptográfica XMLDSig real.
- [ ] Escribir documentación detallada del Dominio y arquitectura.
- [ ] Agregar flujos CI/CD con GitHub Actions (SonarQube y Auto-Releases).
