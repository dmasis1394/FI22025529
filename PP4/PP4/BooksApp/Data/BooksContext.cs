using Microsoft.EntityFrameworkCore;
using BooksApp.Models;

namespace BooksApp.Data;

public class BooksContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TitleTag> TitlesTags { get; set; }

    public string DbPath { get; }

    public BooksContext()
    {
        var folder = Environment.CurrentDirectory;
        var path = Path.Combine(folder, "data");
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        DbPath = Path.Combine(path, "books.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar el nombre de la tabla para TitleTag como TitlesTags
        modelBuilder.Entity<TitleTag>()
            .ToTable("TitlesTags");

        // Configurar el orden de las columnas para Title
        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.TitleId).HasColumnOrder(0);
            entity.Property(e => e.AuthorId).HasColumnOrder(1);
            entity.Property(e => e.TitleName).HasColumnOrder(2);
        });

        base.OnModelCreating(modelBuilder);
    }

    // Método para asegurar que la base de datos se crea
    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
}