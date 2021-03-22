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
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await FindAll()
               .OrderBy(x => x.UserName)
               .ToListAsync();
        }
        public async Task<AppUser> GetUserByIdAsync(Guid userId)
        {
            return await FindByCondition(user => user.Id.Equals(userId))
                .FirstOrDefaultAsync();
        }
        public void CreateUser(AppUser user)
        {
            Create(user);
        }
        public void UpdateUser(AppUser user)
        {
            Update(user);
        }
        public void DeleteUser(AppUser user)
        {
            Delete(user);
        }
    }
}
