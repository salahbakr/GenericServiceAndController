using Generics.Dtos;
using Generics.Entities;
using System.Linq.Expressions;

namespace Generics.IServices
{
    public interface IGenericService<TInput, Entity, TOutput>
        where TInput : BaseCreateDto
        where Entity : BaseEntity
        where TOutput : BaseResponseDto
    {
        Task<ResponseModel<IEnumerable<TOutput>>> GetAllAsync(string[] includes, Expression<Func<Entity, bool>> criteria = null, int page = 1, int NumberOfItems = 12);
        Task<ResponseModel<TOutput>> GetByIdAsync(int id, string[] includes = null);
        Task<ResponseModel<TOutput>> CreateAsync(TInput entity);
        Task<ResponseModel<TOutput>> UpdateAsync(TInput entity, int id);
        Task<ResponseModel<TOutput>> DeleteAsync(int id);
    }
}
