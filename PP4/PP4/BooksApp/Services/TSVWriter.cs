using Microsoft.EntityFrameworkCore;
using BooksApp.Models;
using BooksApp.Data;

namespace BooksApp.Services;

public class TSVWriter : ITSVWriter
{
    public void WriteTSVFiles(string outputDirectory, BooksContext context)
    {
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        // Obtener todos los datos ordenados según las especificaciones
        var data = context.TitlesTags
            .Include(tt => tt.Title)
                .ThenInclude(t => t.Author)
            .Include(tt => tt.Tag)
            .Select(tt => new
            {
                AuthorName = tt.Title.Author.AuthorName,
                TitleName = tt.Title.TitleName,
                TagName = tt.Tag.TagName
            })
            .AsEnumerable()
            .OrderByDescending(x => x.AuthorName, StringComparer.OrdinalIgnoreCase)
            .ThenByDescending(x => x.TitleName, StringComparer.OrdinalIgnoreCase)
            .ThenByDescending(x => x.TagName, StringComparer.OrdinalIgnoreCase)
            .ToList();

        // Agrupar por primera letra del autor
        var groupedData = data.GroupBy(x => GetFirstLetter(x.AuthorName));

        foreach (var group in groupedData)
        {
            var fileName = $"{group.Key}.tsv";
            var filePath = Path.Combine(outputDirectory, fileName);

            using var writer = new StreamWriter(filePath);
            
            // Escribir encabezado
            writer.WriteLine("AuthorName\tTitleName\tTagName");
            
            // Escribir datos
            foreach (var item in group)
            {
                writer.WriteLine($"{EscapeTSVField(item.AuthorName)}\t{EscapeTSVField(item.TitleName)}\t{EscapeTSVField(item.TagName)}");
            }
        }
    }

    private static string GetFirstLetter(string authorName)
    {
        if (string.IsNullOrEmpty(authorName))
            return "Unknown";
        
        // Tomar el primer carácter y convertirlo a mayúscula
        return authorName[0].ToString().ToUpperInvariant();
    }

    private static string EscapeTSVField(string field)
    {
        if (field.Contains('\t') || field.Contains('\n') || field.Contains('\r') || field.Contains('"'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }
}