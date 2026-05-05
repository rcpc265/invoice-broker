# Arquitectura del Sistema: InvoiceBroker

Este proyecto sigue los principios de **Clean Architecture** y **Domain-Driven Design (DDD)**. 

## Capas de la Arquitectura

1. **Domain Layer:** Contiene la lógica central de negocio (entidades, Value Objects) sin dependencias externas. 
   - *Ejemplo:* `Comprobante`, `Serie`, `Correlativo`. Todos modelados según el estándar UBL 2.1 de SUNAT.

2. **Application Layer:** Orquesta los casos de uso utilizando el patrón CQRS con MediatR.
   - *Ejemplo:* `IssueComprobanteCommand` recibe la petición, `FluentValidation` la valida y el handler interactúa con la BD.

3. **Infrastructure Layer:** Conecta el dominio con el mundo exterior.
   - *Base de Datos:* Entity Framework Core (escrituras) y Dapper (lecturas).
   - *Simuladores:* `MockSunatService` y `XmlSigner` simulan el comportamiento del web service de SUNAT.
   - *Resiliencia:* Uso de `Polly` para reintentos y circuit breaker.

4. **API Layer:** Minimal APIs en ASP.NET Core 8 exponiendo los endpoints.
