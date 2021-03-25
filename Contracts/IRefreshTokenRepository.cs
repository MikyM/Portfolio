using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRefreshTokenRepository //: IBaseRepository<RefreshToken>
    {
        Task<IEnumerable<RefreshToken>> GetAllTokensAsync();
        Task<RefreshToken> GetTokenByIdAsync(Guid tokenId);
        void UpdateToken(RefreshToken token);
        void DeleteToken(RefreshToken token);
        void CreateToken(RefreshToken token);
    }
}
