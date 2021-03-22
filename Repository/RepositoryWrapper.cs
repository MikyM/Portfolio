using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ISkillRepository _skill;
        private IRefreshTokenRepository _token;

        public ISkillRepository Skills {
            get {
                if (_skill == null) {
                    _skill = new SkillRepository(_repoContext);
                }
                return _skill;
            }
        }
        public IRefreshTokenRepository RefreshTokens {
            get {
                if (_token == null) {
                    _token = new RefreshTokenRepository(_repoContext);
                }
                return _token;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}