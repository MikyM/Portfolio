using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(Guid userId);
        void UpdateUser(AppUser user);
        void DeleteUser(AppUser user);
        void CreateUser(AppUser user);
    }
}
