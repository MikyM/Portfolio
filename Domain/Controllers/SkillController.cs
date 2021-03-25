using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Repository.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var skillsResult = await _skillService.GetAsync();
            return Ok(skillsResult);
        }

        [HttpGet("{id:guid}", Name = "SkillById")]
        public async Task<IActionResult> GetSkillById(Guid id)
        {
            var skillResult = await _skillService.GetById(id);
            return Ok(skillResult);
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
