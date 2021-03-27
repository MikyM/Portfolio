using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Entities.Wrappers;
using Entities.Filters;
using System.Linq;
using System.Collections.Generic;
using Domain.Services;
using Domain.Helpers;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;
        private IUriService _uriService;
        public SkillController(ISkillService skillService, IUriService uriService)
        {
            _skillService = skillService;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var skillsResult = await _skillService.GetAll(validFilter);
            var pagedReponse = PaginationHelper.CreatePagedReponse(skillsResult.Skills.ToList(), validFilter, skillsResult.TotalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id:guid}", Name = "SkillById")]
        public async Task<IActionResult> GetSkillById(Guid id)
        {
            var skillResult = await _skillService.GetById(id);
            return Ok(new Response<SkillDto>(skillResult));
        }

        [HttpGet("{type:int}", Name = "SkillsByType")]
        public async Task<IActionResult> GetSkillsByType(SkillType type)
        {
            var skillsResult = await _skillService.Where(x => x.Type == type);
            return Ok(skillsResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] SkillDto skill)
        {
            if (!Enum.IsDefined(typeof(SkillType), skill.Type)) {
                return BadRequest();
            }

            var createdSkill = await _skillService.AddOrUpdate(skill);
            return CreatedAtRoute("SkillById", new { id = createdSkill.Id }, createdSkill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill([FromBody] SkillDto skill)
        {
            if (!Enum.IsDefined(typeof(SkillType), skill.Type)) {
                return BadRequest();
            }
            await _skillService.AddOrUpdate(skill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            await _skillService.Remove(id);
            return NoContent();
        }
    }
}
