using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    { 
        public RefreshTokenRepository(RepositoryContext repositoryContext, IErrorHandler errorHandler)
            : base(repositoryContext, errorHandler)
        {
        }

        public async Task<IEnumerable<RefreshToken>> GetAllTokensAsync()
        {
            return await GetAll()
               .OrderBy(x => x.Id)
               .ToListAsync();
        }
        public async Task<RefreshToken> GetTokenByIdAsync(Guid tokenId)
        {
            return await GetTokenByIdAsync(tokenId);
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
