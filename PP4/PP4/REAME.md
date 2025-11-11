# Procesador de Libros - PP4

**Nombre:** Diego Masis Rivas 
**Carné:** FI22025529

## Comandos dotnet utilizados

```bash
# Crear solución y proyecto
dotnet new sln -n BooksSolution
dotnet new console -n BooksApp
dotnet sln add BooksApp/BooksApp.csproj

# Instalar herramientas EF Core
dotnet tool install --global dotnet-ef

# Gestionar migraciones
dotnet ef migrations add InitialCreate
dotnet ef database update

# Compilar y ejecutar
dotnet build
dotnet run

Respuestas a las preguntas
1. ¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?
Code First con NoSQL:
En mi opinión, usar Code First con bases de datos NoSQL como MongoDB sería bastante natural. Pienso que es como diseñar primero cómo quiero organizar mi información y luego la base de datos se adapta a esa estructura. Esta flexibilidad me parece ideal para proyectos donde los requisitos pueden cambiar frecuentemente.

Database First con NoSQL:
Por otro lado, Database First me parece que sería más complicado. Sería como intentar entender una base de datos que ya existe y generar código a partir de ella, pero con el problema de que en NoSQL cada documento puede tener estructura diferente. Creo que esto generaría mucho trabajo adicional y posiblemente código difícil de mantener.

Sobre las Foreign Keys:
Aquí veo el mayor desafío. Las Foreign Keys como las conocemos en bases relacionales simplemente no existen en NoSQL. En mi experiencia, tendría que manejar manualmente las relaciones entre datos y asegurarme yo mismo de la integridad de la información. Esto significa más código y más responsabilidad para el desarrollador.

2. ¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué?
Después de investigar varias alternativas, considero que estas opciones serían útiles:

Pipe (|) con extensión .pipe - Me parece muy práctico porque es fácil de identificar visualmente

Punto y coma (;) con extensión .ssv - Lo veo especialmente útil en países donde se usa coma como separador decimal

Caracteres especiales como el U+001F con extensión .usv - Aunque poco común, sería muy seguro

Doble pipe (||) con extensión .dsv - Menos propenso a conflictos

Mi elección personal:
Si tuviera que elegir uno, me quedaría con el Pipe (|) usando la extensión .pipe. Las razones por las que prefiero esta opción son:

Es muy fácil de leer cuando reviso los archivos manualmente

En la práctica, es raro encontrar datos que contengan pipes naturalmente

Es ampliamente reconocido como separador alternativo

Desde el punto de vista de programación, es sencillo de implementar

En resumen, el pipe me parece el balance perfecto entre simplicidad y efectividad para separar valores en archivos de texto.