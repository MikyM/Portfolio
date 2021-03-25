using System;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class SkillDto : BaseResponseDto
    {
         public string Name { get; set; }
         public SkillType Type { get; set; }
         public string ImagePath { get; set; }
    }
}
