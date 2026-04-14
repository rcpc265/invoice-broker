# InvoiceBroker

> Middleware asíncrono para emisión de facturación electrónica a SUNAT (Perú) con UBL 2.1.

## 🚀 Inicio Rápido (Para Reclutadores / Devs sin Docker)

No necesitas configurar nada complejo. El proyecto incluye una base de datos en memoria para pruebas rápidas.

1. Clona el repositorio.
2. Abre una terminal en la raíz del proyecto.
3. Ejecuta:
   ```bash
   dotnet run --project src/InvoiceBroker.Api
   ```
4. Abre tu navegador en: **http://localhost:5000/scalar/v1**

Verás el **Panel Interactivo (Scalar)** donde podrás probar la API enviando peticiones JSON con un solo clic.

## 📦 Arquitectura
Este proyecto sigue estrictamente los principios de **Clean Architecture** (Dominio, Aplicación, Infraestructura, API) y **Domain-Driven Design (DDD)** (Value Objects, Encapsulamiento, Inmutabilidad).
