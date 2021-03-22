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

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public SkillController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _repository.Skills.GetAllSkillsAsync();
            var skillsResult = _mapper.Map<IEnumerable<SkillDto>>(skills);
            return Ok(skillsResult);
        }

        [HttpGet("{id:guid}", Name = "SkillById")]
        public async Task<IActionResult> GetSkillById(Guid id)
        {
            var skill = await _repository.Skills.GetSkillByIdAsync(id);
            var skillResult = _mapper.Map<SkillDto>(skill);
            return Ok(skillResult);
        }

        [HttpGet("{type:int}", Name = "SkillsByType")]
        public async Task<IActionResult> GetSkillsByType(SkillType type)
        {
            var skills = await _repository.Skills.GetSkillsByTypeAsync(type);
            var skillsResult = _mapper.Map<IEnumerable<SkillDto>>(skills);
            return Ok(skillsResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] SkillForCreationDto skill)
        {
            if (!Enum.IsDefined(typeof(SkillType), skill.Type)) {
                return BadRequest();
            }
            var skillEntity = _mapper.Map<Skill>(skill);
            _repository.Skills.CreateSkill(skillEntity);
            await _repository.SaveAsync();
            var createdSkill = _mapper.Map<SkillDto>(skillEntity);
            return CreatedAtRoute("SkillById", new { id = createdSkill.Id }, createdSkill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(Guid id, [FromBody] SkillForUpdateDto skill)
        {
            if (!Enum.IsDefined(typeof(SkillType), skill.Type)) {
                return BadRequest();
            }
            var skillEntity = await _repository.Skills.GetSkillByIdAsync(id);
            _mapper.Map(skill, skillEntity);
            _repository.Skills.UpdateSkill(skillEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            var skill = await _repository.Skills.GetSkillByIdAsync(id);
            _repository.Skills.DeleteSkill(skill);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
