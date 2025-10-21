**Nombre:** Diego Masis Rivas 
**Carné:** FI22025529

## Comandos dotnet utilizados

```bash
# Crear solución
dotnet new sln -n TextProcessor

# Crear proyecto web API
dotnet new web -n TextProcessor.API -f net8.0

# Agregar proyecto a solución
dotnet sln add TextProcessor.API/TextProcessor.API.csproj

# Agregar Swagger
dotnet add package Swashbuckle.AspNetCore

# Ejecutar
dotnet run

# Recursos consultados
- Documentación oficial de ASP.NET Core Minimal APIs

- Swagger/OpenAPI con Minimal APIs

#Preguntas y respuestas

#¿Es posible enviar valores en el Body (por ejemplo, en el Form) del Request de tipo GET?

No, según el estándar HTTP, los métodos GET no deben tener body. Los parámetros deben enviarse en la URL como query parameters.

#¿Qué ventajas y desventajas observa con el Minimal API si se compara con la opción de utilizar Controllers?

#Ventajas:
- Menos código boilerplate

- Mejor rendimiento

- Más fácil de entender para APIs simples

- Configuración más directa

#Desventajas:
- Puede volverse complicado para APIs muy grandes

- Menos estructura organizativa

- Limitado en características avanzadas