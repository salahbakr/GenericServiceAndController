namespace Generics.Entities
{
    public class Book : BaseEntity
    {
        public required string Name { get; set; }

        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}
