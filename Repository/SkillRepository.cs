using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        public SkillRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await FindAll()
               .OrderBy(x => x.Name)
               .ToListAsync();
        }
        public async Task<IEnumerable<Skill>>GetSkillsByTypeAsync(SkillType type)
        {
            return await FindByCondition(skill => skill.Type.Equals(type))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public async Task<Skill> GetSkillByIdAsync(Guid skillId)
        {
            return await FindByCondition(skill => skill.Id.Equals(skillId))
                .FirstOrDefaultAsync();
        }
        public void CreateSkill(Skill Skill)
        {
            Create(Skill);
        }
        public void UpdateSkill(Skill Skill)
        {
            Update(Skill);
        }
        public void DeleteSkill(Skill Skill)
        {
            Delete(Skill);
        }
    }
}
