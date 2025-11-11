using BooksApp.Data;

namespace BooksApp.Services;

public interface ITSVWriter
{
    void WriteTSVFiles(string outputDirectory, BooksContext context);
}