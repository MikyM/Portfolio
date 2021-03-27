using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected RepositoryContext RepositoryContext { get; set; }
        private readonly IErrorHandler _errorHandler;

        public BaseRepository(RepositoryContext repositoryContext, IErrorHandler errorHandler)
        {
            this.RepositoryContext = repositoryContext;
            this._errorHandler = errorHandler;
        }
        public Task<TEntity> GetById(Guid id)
        {
            return Task.FromResult(this.RepositoryContext.Set<TEntity>().AsNoTracking().SingleOrDefault(x => x.Id == id));
        }
        public IQueryable<TEntity> GetAll()
        {
            return this.RepositoryContext.Set<TEntity>().AsNoTracking();
        }
        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return this.RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking();
        }
        public async Task<int> CountAsync()
        {
            return await this.RepositoryContext.Set<TEntity>().CountAsync();
        }
        public void Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            this.RepositoryContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            this.RepositoryContext.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(string.Format(_errorHandler.GetMessage(ErrorMessagesEnum.EntityNull), "", "Input data is null"));
            this.RepositoryContext.Set<TEntity>().Remove(entity);
        }
    }
}