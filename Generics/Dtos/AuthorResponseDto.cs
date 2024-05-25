namespace Generics.Dtos;

public class AuthorResponseDto : BaseResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<BookResponseDto> Books { get; set; }
}
