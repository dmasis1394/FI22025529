using System;

namespace SumCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Búsqueda de sumas válidas de números naturales\n");
            
            Console.WriteLine("• SumFor:");
            FindValidSumsOptimized(SumFor);
            
            Console.WriteLine("• SumIte:");
            FindValidSumsOptimized(SumIte);
        }
        
        static int SumFor(int n) => n * (n + 1) / 2;
        
        static int SumIte(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++) sum += i;
            return sum;
        }
        
        static void FindValidSumsOptimized(Func<int, int> sumMethod)
        {
            
            // Ascendente:
            int lastValidN = FindLastValidAscending(sumMethod);
            Console.WriteLine($"        ◦ From 1 to Max → n: {lastValidN} → sum: {sumMethod(lastValidN)}");
            
            // Descendente:
            int firstValidN = FindFirstValidDescending(sumMethod, lastValidN);
            Console.WriteLine($"        ◦ From Max to 1 → n: {firstValidN} → sum: {sumMethod(firstValidN)}");
        }
        
        static int FindLastValidAscending(Func<int, int> sumMethod)
        {
            for (int n = 1; n <= int.MaxValue; n++)
            {
                if (sumMethod(n) <= 0) return n - 1;
            }
            return 0;
        }
        
        static int FindFirstValidDescending(Func<int, int> sumMethod, int startFrom)
        {
            const int Max = int.MaxValue;
            const int Step = 1000000;
            
            // Primero: búsqueda con saltos grandes
            for (int n = Max; n > startFrom; n -= Step)
            {
                if (sumMethod(n) > 0)
                {
                    // Encontramos un rango válido, ahora buscamos exactamente
                    for (int i = n; i >= Math.Max(n - Step, startFrom); i--)
                    {
                        if (sumMethod(i) > 0) return i;
                    }
                }
            }
            
            // Si no encontró en los saltos grandes, busca secuencial desde startFrom
            for (int n = startFrom; n <= Max; n++)
            {
                if (sumMethod(n) > 0) return n;
            }
            
            return 0;
        }
    }
}