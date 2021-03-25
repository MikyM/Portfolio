using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISkillRepository Skills { get; }
        Task CommitAsync();
        void Rollback();
    }
}
