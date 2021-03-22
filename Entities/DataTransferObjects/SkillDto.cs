using System;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class SkillDto
    {
         public Guid Id { get; set; }
         public string Name { get; set; }
         public SkillType Type { get; set; }
         public string ImagePath { get; set; }
         public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
