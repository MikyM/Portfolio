using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Repositories;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private RepositoryContext _dbContext;
        private ISkillRepository _skills;
        private IErrorHandler _errorHandler;

        public UnitOfWork(RepositoryContext dbContext, IErrorHandler errorHandler)
        {
            _dbContext = dbContext;
            _errorHandler = errorHandler;
        }

        public ISkillRepository Skills {
            get {
                return _skills ??
                    (_skills = new SkillRepository(_dbContext, _errorHandler));
            }
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()) {
                switch (entry.State) {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
