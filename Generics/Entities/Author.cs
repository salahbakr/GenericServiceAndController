namespace Generics.Entities;

public class Author : BaseEntity
{
    public required string Name { get; set; }

    public virtual ICollection<Book>? Books { get; set; }
}
