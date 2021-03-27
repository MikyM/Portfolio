using Contracts;
using Entities;
using Entities.Filters;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll(PaginationFilter filter)
        {
            return await _repository.GetAll()
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> exp)
        {
            return await _repository.GetByCondition(exp).ToListAsync();
        }

        public void AddOrUpdate(T entry)
        {
            var targetRecord = _repository.GetById(entry.Id).Result;
            var exists = targetRecord != null;

            if (exists) {
                entry.DateModified = DateTime.UtcNow;
                _repository.Update(entry);
                return;
            }

            entry.DateAdded = DateTime.UtcNow;
            _repository.Create(entry);
        }

        public void Remove(Guid id)
        {
            var label = _repository.GetById(id).Result;
            _repository.Delete(label);
        }
    }
}
