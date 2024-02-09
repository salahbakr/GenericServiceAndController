using Generics.Dtos;
using Generics.Entities;
using Generics.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Generics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TInput, Entity, TOutput> : ControllerBase
        where TInput : BaseCreateDto
        where Entity : BaseEntity
        where TOutput : BaseResponseDto
    {
        private readonly IGenericService<TInput, Entity, TOutput> _genericService;

        public GenericController(IGenericService<TInput, Entity, TOutput> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int page = 1, int NumberOfItems = 12)
        {
            var result = await _genericService.GetAllAsync(null, null, page, NumberOfItems);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("id ={id}")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            var result = await _genericService.GetByIdAsync(id, null);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TInput createDto)
        {
            var result = await _genericService.CreateAsync(createDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] TInput createDto, int id)
        {
            var result = await _genericService.UpdateAsync(createDto, id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _genericService.DeleteAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
