using Entities.DataTransferObjects;
using Entities.Filters;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISkillService
    {
        Task<Skill> AddOrUpdate(SkillDto entry);
        Task<(IEnumerable<SkillDto> Skills, int TotalRecords)> GetAll(PaginationFilter filter);
        Task<SkillDto> GetById(Guid id);
        Task Remove(Guid id);
        Task<IEnumerable<SkillDto>> Where(Expression<Func<Skill, bool>> exp);
    }
}
