using Generics.Attributes;

namespace Generics.Dtos
{
    [GenericControllerRoute("api/Books")]
    public class BookCreateDto : BaseCreateDto
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
    }
}
