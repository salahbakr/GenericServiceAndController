namespace Generics.Dtos
{
    public class BookResponseDto : BaseResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
