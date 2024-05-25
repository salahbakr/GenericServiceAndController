using Generics.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Generics.Dtos;

[GenericControllerRoute("api/Authors")]
public class AuthorCreateDto : BaseCreateDto
{
    [Required]
    public required string Name { get; set; }
}
