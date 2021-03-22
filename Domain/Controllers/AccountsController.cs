using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Domain.Helpers;

namespace Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private UserManager<AppUser> _userManager;
        public AccountsController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppUserForCreationDto user)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(user);
            var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
