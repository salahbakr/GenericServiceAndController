using AutoMapper;
using Generics.Data;
using Generics.Dtos;
using Generics.Entities;
using Generics.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Generics.Services
{
    public class GenericService<TInput, Entity, TOutput> : IGenericService<TInput, Entity, TOutput>
        where TInput : BaseCreateDto
        where Entity : BaseEntity
        where TOutput : BaseResponseDto
    {

        ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenericService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<TOutput>>> GetAllAsync(string[] includes, Expression<Func<Entity, bool>> criteria = null, int page = 1, int NumberOfItems = 12)
        {
            IQueryable<Entity> query = _dbContext.Set<Entity>();
            query = BuildQuery(query, includes, criteria);

            var records = await query.Skip((page - 1) * NumberOfItems).Take(NumberOfItems).ToListAsync();

            if (records .Count() == 0)
                return new ResponseModel<IEnumerable<TOutput>> 
                {
                    Errors = "Error while retrieving data",
                    Message = "There are no records"
                };

            return new ResponseModel<IEnumerable<TOutput>>
            {
                Message = "Retrieved data successfully",
                Data = _mapper.Map<IEnumerable<TOutput>>(records)
            };
        }

        public async Task<ResponseModel<TOutput>> GetByIdAsync(int id, string[] includes = null)
        {

            var record = await _dbContext.Set<Entity>().FindAsync(id);

            if (record is null)
                return new ResponseModel<TOutput>
                {
                    Errors = "Error while retrieving data",
                    Message = "No record with that id"
                };

            return new ResponseModel<TOutput>
            {
                Message = "Retrieved data successfully",
                Data = _mapper.Map<TOutput>(record)
            };
        }

        public async Task<ResponseModel<TOutput>> CreateAsync(TInput entityDto)
        {
            var record = _mapper.Map<Entity>(entityDto);

            try
            {
                _dbContext.Add(record);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<TOutput>
                {
                    Errors = "Error while adding a new record",
                    Message = "Something went wrong"
                };
            }

            return new ResponseModel<TOutput>
            {
                Message = "Successfully added a new record",
                Data = _mapper.Map<TOutput>(record)
            };
        }

        public async Task<ResponseModel<TOutput>> UpdateAsync(TInput entityDto, int id)
        {
            var record = await _dbContext.Set<Entity>().FindAsync(id);

            if (record is null)
                return new ResponseModel<TOutput>
                {
                    Errors = "Error while updating record",
                    Message = "No record with that id"
                };

            _mapper.Map(entityDto, record);

            try
            {
                _dbContext.Update(record);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ResponseModel<TOutput>
                {
                    Errors = "Error while updating the record",
                    Message = "Something went wrong"
                };
            }

            return new ResponseModel<TOutput>
            {
                Message = "Successfully updated the record",
                Data = _mapper.Map<TOutput>(record)
            };
        }

        public async Task<ResponseModel<TOutput>> DeleteAsync(int id)
        {
            var record = await _dbContext.Set<Entity>().FindAsync(id);

            if (record is null)
                return new ResponseModel<TOutput>
                {
                    Errors = "Error while updating record",
                    Message = "No record with that id"
                };

            try
            {
                _dbContext.Remove(record);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new ResponseModel<TOutput>
                {
                    Errors = "Error while deleting the record",
                    Message = "Something went wrong"
                };
            }

            return new ResponseModel<TOutput>
            {
                Message = "Successfully deleted the record",
                Data = _mapper.Map<TOutput>(record)
            };
        }

        internal IQueryable<Entity> BuildQuery(IQueryable<Entity> query, string[] includes, Expression<Func<Entity, bool>> criteria)
        {
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (criteria != null)
                query = query.Where(criteria);

            return query;
        }
    }
}
