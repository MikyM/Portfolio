using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserService
    {
        IQueryable<AppUserDto> Get();
        AppUserDto GetByEmail(string email);
        Task<IdentityResult> Create(AppUserDto user, string password);
        Task<IdentityResult> Delete(AppUserDto user);
        Task<IdentityResult> Update(AppUserDto user);
    }
}
