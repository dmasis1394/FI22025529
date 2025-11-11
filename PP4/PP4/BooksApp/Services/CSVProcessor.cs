using System.Globalization;

namespace BooksApp.Services;

public class CSVProcessor : ICSVProcessor
{
    public List<BookRecord> ReadBooksFromCSV(string filePath)
    {
        var books = new List<BookRecord>();
        
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"El archivo CSV no fue encontrado: {filePath}");
        }

        var lines = File.ReadAllLines(filePath);
        
        // Saltar la primera línea (encabezado)
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line)) continue;

            var fields = ParseCSVLine(line);
            
            if (fields.Count >= 3)
            {
                var book = new BookRecord(
                    fields[0].Trim(),
                    fields[1].Trim(),
                    fields[2].Trim()
                );
                books.Add(book);
            }
        }

        return books;
    }

    private List<string> ParseCSVLine(string line)
    {
        var fields = new List<string>();
        var currentField = new System.Text.StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                currentField.Append(c);
            }
        }

        fields.Add(currentField.ToString());
        return fields;
    }
}