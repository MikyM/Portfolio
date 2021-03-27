﻿using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<AppUser> Get() => _userManager.Users;

        public AppUser GetByEmail(string email) => _userManager.Users.First(u => u.Email == email);

        public Task<IdentityResult> Create(AppUser user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> Delete(AppUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> Update(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public UserManager<AppUser> GetUserManager()
        {
            return _userManager;
        }
    }
}
