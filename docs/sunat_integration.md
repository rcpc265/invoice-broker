# Integración con SUNAT: Facturación Electrónica

El ecosistema de SUNAT para la recepción de Comprobantes de Pago Electrónicos (CPE) está basado en estándares internacionales y criptografía. Este proyecto simula esos flujos críticos.

## 1. UBL 2.1 (Universal Business Language)
SUNAT utiliza el estándar UBL 2.1 de OASIS para definir la estructura XML de las facturas, boletas, notas de crédito y débito.
- Nuestro generador interno asegura que los datos (Totales, IGV, RUCs) se apeguen estrictamente a los catálogos oficiales de SUNAT.

## 2. Firma Digital (XMLDSig)
Para garantizar la integridad y el no repudio, cada XML UBL 2.1 debe ser firmado usando criptografía asimétrica (certificado digital X.509).
- La firma se incrusta dentro del tag `<ext:ExtensionContent>` usando el estándar XMLDSig.

## 3. Web Service SOAP y Resiliencia
El envío del comprobante firmado se realiza a través de un servicio web SOAP (`billService`). 
- **Problema común:** Los servidores de SUNAT suelen tener tiempos de inactividad o lentitud.
- **Solución implementada:** Uso de `Polly` para aplicar políticas de reintentos (Retry) y cortacircuitos (Circuit Breaker).
