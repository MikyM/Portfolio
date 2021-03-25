using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        IQueryable<AppUser> GetAll();
        AppUser GetByEmail(string email);
        AppUser GetById(Guid id);
        Task<IdentityResult> Create(AppUser user, string password);
        Task<IdentityResult> Delete(AppUser user);
        Task<IdentityResult> Update(AppUser user);
        UserManager<AppUser> GetUserManager();
    }
}
