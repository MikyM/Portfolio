using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface ISkillRepository : IBaseRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<Skill> GetSkillByIdAsync(Guid skillId);
        Task<IEnumerable<Skill>> GetSkillsByTypeAsync(SkillType skillType);
        Task<int> CountSkillsAsync();
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
        void CreateSkill(Skill skill);
    }
}
