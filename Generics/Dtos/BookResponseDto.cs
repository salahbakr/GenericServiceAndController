namespace Generics.Dtos
{
    public class BookResponseDto : BaseResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        //public AuthorResponseDto Author { get; set; }
    }
}
