using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum SkillType
    {
        Language,
        Framework,
        Tool,
        Soft
    }

    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public SkillType Type { get; set; } 
        public string ImagePath { get; set; }
    }
}
