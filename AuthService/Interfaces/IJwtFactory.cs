using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        Task<RefreshToken> GenerateRefreshToken(string ipAddress);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, List<string> roles);
    }
}
