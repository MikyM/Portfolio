using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Filters;
using Entities.Models;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SkillService : ISkillService
    {
        private readonly IBaseService<Skill> _service;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWok;


        public SkillService(IBaseService<Skill> service, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _service = service;
            _mapper = mapper;
            _unitOfWok = unitOfWork;
        }

        public async Task<(IEnumerable<SkillDto> Skills, int TotalRecords)> GetAll(PaginationFilter filter)
        {
            var result = await _service.GetAll(filter);
            var totalRecords = await _unitOfWok.Skills.CountAsync();
            return (result.Select(t => _mapper.Map<Skill, SkillDto>(t)), totalRecords);
        }

        public async Task<SkillDto> GetById(Guid id)
        {
            return _mapper.Map<Skill, SkillDto>(await _service.GetById(id));
        }

        public async Task<IEnumerable<SkillDto>> Where(Expression<Func<Skill, bool>> exp)
        {
            var whereResult = await _service.Where(exp);
            return _mapper.Map<List<Skill>, List<SkillDto>>(whereResult.ToList()).AsEnumerable();
        }

        public async Task<Skill> AddOrUpdate(SkillDto entry)
        {
            var skill = _mapper.Map<SkillDto, Skill>(entry);
            _service.AddOrUpdate(skill);
            await _unitOfWok.CommitAsync();
            return skill;
        }

        public async Task Remove(Guid id)
        {
            _service.Remove(id);
            await _unitOfWok.CommitAsync();
        }
    }
}
