using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models;

public class Tag
{
    [Key]
    public int TagId { get; set; }
    
    [Required]
    public string TagName { get; set; } = string.Empty;
    
    public ICollection<TitleTag> TitleTags { get; set; } = new List<TitleTag>();
}