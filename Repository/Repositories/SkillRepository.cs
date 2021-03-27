using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(RepositoryContext repositoryContext, IErrorHandler errorHandler)
            : base(repositoryContext, errorHandler)
        {
        }
        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await GetAll()
               .OrderBy(x => x.Name)
               .ToListAsync();
        }
        public async Task<IEnumerable<Skill>> GetSkillsByTypeAsync(SkillType type)
        {
            return await GetByCondition(skill => skill.Type.Equals(type))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public async Task<Skill> GetSkillByIdAsync(Guid skillId)
        {
            return await GetByCondition(skill => skill.Id.Equals(skillId))
                .FirstOrDefaultAsync();
        }
        public async Task<int> CountSkillsAsync()
        {
            return await CountAsync();
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
