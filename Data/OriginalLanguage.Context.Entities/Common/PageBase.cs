namespace OriginalLanguage.Context.Entities;
public abstract class PageBase : EntityBase
{
    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public DateTime DateTimeAdded { get; set; } = DateTime.UtcNow;
}
