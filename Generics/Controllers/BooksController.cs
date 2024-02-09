using Generics.Dtos;
using Generics.Entities;
using Generics.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Generics.Controllers
{
    public class BooksController : GenericController<BookCreateDto, Book, BookResponseDto>
    {
        private readonly IGenericService<BookCreateDto, Book, BookResponseDto> _bookService;

        public BooksController(IGenericService<BookCreateDto, Book, BookResponseDto> bookService)
            : base(bookService)
        {
            _bookService = bookService;
        }
    }
}
