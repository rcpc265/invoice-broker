# InvoiceBroker

> Middleware asíncrono para emisión de facturación electrónica a SUNAT (Perú) con UBL 2.1.

## 🚀 Inicio Rápido (Para Reclutadores / Devs sin Docker)

No se requiere configuración compleja. El proyecto incluye una base de datos en memoria para pruebas rápidas.

1. Clonar el repositorio.
2. Abrir una terminal en la raíz del proyecto.
3. Ejecutar:
   ```bash
   dotnet run --project src/InvoiceBroker.Api
   ```
4. Navegar a la siguiente dirección: **http://localhost:5000/scalar/v1** o **http://localhost:5000/swagger**

Se mostrará el **Panel Interactivo** donde es posible probar la API enviando peticiones JSON pre-configuradas con un solo clic.

### Opción 2: Ejecución con Docker
Para levantar la solución completa junto con la base de datos (SQL Server) utilizando contenedores:

```bash
docker-compose up -d --build
```
La API estará expuesta en el puerto 8080 (o el configurado en Docker).

## 📦 Arquitectura
Este proyecto sigue estrictamente los principios de **Clean Architecture** (Dominio, Aplicación, Infraestructura, API) y **Domain-Driven Design (DDD)** (Value Objects, Encapsulamiento, Inmutabilidad).
