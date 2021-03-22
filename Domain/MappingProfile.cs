﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Server
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Skill, SkillDto>();
            CreateMap<SkillForCreationDto, Skill>();
            CreateMap<SkillForUpdateDto, Skill>();
            CreateMap<AppUserForCreationDto, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
