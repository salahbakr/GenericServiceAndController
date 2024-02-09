using AutoMapper;
using Generics.Dtos;
using Generics.Entities;

namespace Generics.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookCreateDto, Book>();
            CreateMap<Book, BookResponseDto>();
        }
    }
}
