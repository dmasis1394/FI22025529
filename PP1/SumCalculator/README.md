# PP1 - Calculadora de Sumas de Números Naturales

## Información del estudiante
- **Nombre:** DIego Masis Rivas
- **Carné:** FI22025529

## Comandos dotnet utilizados
```bash
dotnet new sln -n PP1
dotnet new console -n SumCalculator
dotnet sln add SumCalculator/SumCalculator.csproj
dotnet build
dotnet run

// Recursos utilizados:
Documentación oficial de Microsoft sobre C#: https://docs.microsoft.com/dotnet/csharp/

Stack Overflow para consultas sobre algoritmos y optimización

Documentación de .NET CLI: https://docs.microsoft.com/dotnet/core/tools/

1. ¿Por qué todos los valores resultantes difieren entre métodos y estrategias?
Los valores son diferentes por dos razones principales:

Diferencia entre los métodos de cálculo (SumFor vs SumIte)
SumFor usa una fórmula matemática directa: n × (n + 1) ÷ 2

SumIte suma número por número: 1 + 2 + 3 + ... + n

¿Por qué fallan en momentos distintos?

SumFor falla cuando el resultado de la multiplicación n × (n + 1) se hace demasiado grande para ser representado como número entero, incluso antes de hacer la división.

SumIte falla antes porque va acumulando la suma paso a paso. En algún punto, al agregar el siguiente número, la suma total se vuelve demasiado grande y se desborda.

Cada método tiene su propio límite donde los números se hacen demasiado grandes.

Diferencia entre las estrategias (ascendente vs descendente)
Ascendente (desde 1 hacia arriba): Encuentra el último valor de n que produce una suma válida antes de que empiecen a salir resultados incorrectos.

Descendente (desde el máximo hacia abajo): Encuentra el primer valor de n que produce una suma válida cuando empiezas desde el número más grande posible.

Estás buscando puntos diferentes en la misma escala de números, por eso encuentras resultados distintos. //

// 2. ¿Qué sucedería con el método recursivo (SumRec)?
El método recursivo tendría problemas graves de funcionamiento:

El problema de la recursión
Cada llamada recursiva ocupa espacio en la memoria (en la "pila de llamadas")

Este espacio es limitado y mucho más pequeño que el rango de números enteros

En la práctica:
Con números enteros podrías teóricamente usar valores hasta 2,147 millones

Con recursión el sistema solo aguanta entre 1,000 y 10,000 llamadas recursivas antes de colapsar

Lo que pasaría realmente:
Estrategia ascendente: Funcionaría solo para valores muy pequeños de n (hasta unos pocos miles) y luego el programa se cerraría con error de "Stack Overflow".

Estrategia descendente: Sería aún peor. Al intentar empezar desde números gigantes, el programa fallaría inmediatamente porque intentaría hacer demasiadas llamadas recursivas de una vez. //