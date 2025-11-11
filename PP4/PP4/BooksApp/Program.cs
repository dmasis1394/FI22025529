using BooksApp.Data;
using BooksApp.Services;

// Configurar rutas
var dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");
var csvFilePath = Path.Combine(dataDirectory, "books.csv");

// Crear instancia del contexto
using var context = new BooksContext();

// Crear base de datos automáticamente (sin migraciones)
Console.WriteLine("Creando base de datos...");
context.EnsureDatabaseCreated();

// Verificar si necesitamos inicializar la base de datos o generar archivos TSV
if (!context.Authors.Any())
{
    // Inicializar base de datos desde CSV
    var csvProcessor = new CSVProcessor();
    var initializer = new DatabaseInitializer(context, csvProcessor);
    
    try
    {
        await initializer.InitializeDatabaseAsync(csvFilePath);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"Error: No se encontró el archivo CSV en {csvFilePath}");
        Console.WriteLine("Asegúrese de que el archivo 'books.csv' esté en la carpeta 'data'.");
        return;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error inesperado: {ex.Message}");
        return;
    }
}
else
{
    // Generar archivos TSV
    Console.WriteLine("La base de datos se está leyendo para crear los archivos TSV.");
    Console.Write("Procesando... ");
    
    var tsvWriter = new TSVWriter();
    tsvWriter.WriteTSVFiles(dataDirectory, context);
    
    Console.WriteLine("Listo.");
}

Console.WriteLine("Proceso completado exitosamente.");