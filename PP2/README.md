# Operaciones Binarias - ASP.NET Core MVC

## Información del Estudiante
- **Nombre:** Diego Masis Rivas
- **Carné:** FI22025529

## Comandos dotnet CLI Utilizados

```bash
# Crear solución
dotnet new sln -n BinaryOperations

# Crear proyecto MVC
dotnet new mvc -n BinaryOperations.Web

# Agregar proyecto a la solución
dotnet sln add BinaryOperations.Web/BinaryOperations.Web.csproj

# Restaurar paquetes
dotnet restore

# Ejecutar la aplicación
dotnet run

# Compilar en modo Release
dotnet build -c Release

#Recursos y Referencias
Documentación oficial de ASP.NET Core

Validaciones con Data Annotations

Conversión de bases numéricas en C#

Asistencia de IA:
Consultas realizadas a ChatGPT:
1) ¿Cómo implementar validaciones personalizadas con Data Annotations en ASP.NET Core?

2) ¿Cuál es la mejor manera de realizar operaciones binarias bit a bit en strings con C#?

3) ¿Cómo convertir números entre diferentes bases (binario, octal, hexadecimal) en .NET?

Respuestas obtenidas:
Implementación de CustomValidationAttribute para validaciones complejas

Uso de Convert.ToString(value, base) para conversiones entre bases

Estrategias para operaciones bit a bit manuales en strings

#Respuestas a Preguntas:

1. Multiplicación con valores máximos permitidos:

Los valores máximos permitidos son:

a: 11111111 (8 bits)

b: 11111111 (8 bits)

Resultado de la multiplicación (a • b):

Base	                       Valor
Binario	                  1111111000000001
Octal	                       177401
Decimal	                       65025
Hexadecimal	                    FE01

Cálculo:

- a (decimal) = 255

- b (decimal) = 255

- 255 × 255 = 65025

2. Posibilidad de hacer operaciones en otra capa:

Sí, es posible realizar las operaciones en diferentes capas:

Capa de Servicio (Recomendado): Como se implementó en este proyecto, separando la lógica de negocio del controlador.

Capa de Modelo: Implementando la lógica directamente en el modelo, aunque esto puede violar el principio de responsabilidad única.

Capa de Controlador: No recomendado, ya que mezcla lógica de presentación con lógica de negocio.

Microservicios: Podría crearse un servicio separado específico para cálculos binarios.