using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IQueryable<AppUserDto> Get()
        {

            var returnedList = new List<AppUserDto>();
            _repository.Get().ToList().ForEach(u => {
                returnedList.Add(_mapper.Map<AppUser, AppUserDto>(u));
            });

            return returnedList.AsQueryable();
        }

        public AppUserDto GetByEmail(string email)
        {
            return _mapper.Map<AppUser, AppUserDto>(_repository.GetByEmail(email));
        }

        public Task<IdentityResult> Create(AppUserDto user, string password)
        {
            return _repository.Create(_mapper.Map<AppUserDto, AppUser>(user), password);
        }

        public async Task<IdentityResult> Delete(AppUserDto user)
        {
            return await _repository.Delete(_mapper.Map<AppUserDto, AppUser>(user));
        }

        public async Task<IdentityResult> Update(AppUserDto user)
        {
            return await _repository.Update(_mapper.Map<AppUserDto, AppUser>(user));
        }
    }
}
