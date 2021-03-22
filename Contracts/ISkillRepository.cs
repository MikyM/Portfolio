using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISkillRepository : IRepositoryBase<Skill>
    {
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<Skill> GetSkillByIdAsync(Guid skillId);
        Task<IEnumerable<Skill>> GetSkillsByTypeAsync(SkillType skillType);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
        void CreateSkill(Skill skill);
    }
}
