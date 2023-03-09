using Microsoft.EntityFrameworkCore;
using MobvenSozluk.Infrastructure.Exceptions;
using MobvenSozluk.Repository.DTOs.ResponseDTOs;
using MobvenSozluk.Repository.Repositories;
using MobvenSozluk.Repository.Services;
using MobvenSozluk.Repository.UnitOfWorks;
using System.Linq.Expressions;

namespace MobvenSozluk.Infrastructure.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            var entity = _repository.AnyAsync(expression);
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return await entity;

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = _repository.GetAll().ToListAsync();
            if (entities == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return await entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) not found");
            }
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            var entities = _repository.Where(expression);
            if (entities == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return entities;

        }


    }
}
