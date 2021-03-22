using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    { 
        public RefreshTokenRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<RefreshToken>> GetAllTokensAsync()
        {
            return await FindAll()
               .OrderBy(x => x.Id)
               .ToListAsync();
        }
        public async Task<RefreshToken> GetTokenByIdAsync(Guid tokenId)
        {
            return await FindByCondition(token => token.Id.Equals(tokenId))
                .FirstOrDefaultAsync();
        }
        public void CreateToken(RefreshToken token)
        {
            Create(token);
        }
        public void UpdateToken(RefreshToken token)
        {
            Update(token);
        }
        public void DeleteToken(RefreshToken token)
        {
            Delete(token);
        }
    }
}
