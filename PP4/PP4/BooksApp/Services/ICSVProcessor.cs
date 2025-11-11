namespace BooksApp.Services;

public interface ICSVProcessor
{
    List<BookRecord> ReadBooksFromCSV(string filePath);
}

public record BookRecord(string Author, string Title, string Tags);