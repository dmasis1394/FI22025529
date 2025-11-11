using BooksApp.Models;
using BooksApp.Data;

namespace BooksApp.Services;

public class DatabaseInitializer
{
    private readonly BooksContext _context;
    private readonly ICSVProcessor _csvProcessor;

    public DatabaseInitializer(BooksContext context, ICSVProcessor csvProcessor)
    {
        _context = context;
        _csvProcessor = csvProcessor;
    }

    public async Task InitializeDatabaseAsync(string csvFilePath)
    {
        // Verificar si la base de datos ya tiene datos
        if (_context.Authors.Any())
        {
            return;
        }

        Console.WriteLine("La base de datos está vacía, por lo que será llenada a partir de los datos del archivo CSV.");
        Console.Write("Procesando... ");

        var books = _csvProcessor.ReadBooksFromCSV(csvFilePath);
        
        var authorsDict = new Dictionary<string, Author>();
        var tagsDict = new Dictionary<string, Tag>();
        var titles = new List<Title>();
        var titleTags = new List<TitleTag>();

        int authorId = 1;
        int tagId = 1;
        int titleId = 1;

        foreach (var book in books)
        {
            // Procesar autor
            if (!authorsDict.ContainsKey(book.Author))
            {
                authorsDict[book.Author] = new Author { AuthorId = authorId, AuthorName = book.Author };
                authorId++;
            }
            var author = authorsDict[book.Author];

            // Procesar título
            var title = new Title 
            { 
                TitleId = titleId, 
                TitleName = book.Title, 
                AuthorId = author.AuthorId 
            };
            titles.Add(title);

            // Procesar tags
            var tagNames = book.Tags.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var tagName in tagNames)
            {
                if (!tagsDict.ContainsKey(tagName))
                {
                    tagsDict[tagName] = new Tag { TagId = tagId, TagName = tagName };
                    tagId++;
                }
                var tag = tagsDict[tagName];

                titleTags.Add(new TitleTag 
                { 
                    TitleTagId = titleTags.Count + 1,
                    TitleId = title.TitleId, 
                    TagId = tag.TagId 
                });
            }

            titleId++;
        }

        // Guardar en la base de datos
        await _context.Authors.AddRangeAsync(authorsDict.Values);
        await _context.Tags.AddRangeAsync(tagsDict.Values);
        await _context.Titles.AddRangeAsync(titles);
        await _context.TitlesTags.AddRangeAsync(titleTags);

        await _context.SaveChangesAsync();
        Console.WriteLine("Listo.");
    }
}