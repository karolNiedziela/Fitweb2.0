using AutoMapper;
using Fitweb.Application.Commands.Sets.Add;
using Fitweb.Application.DTO;
using Fitweb.Domain.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class SetProfile : Profile
    {
        public SetProfile()
        {
            CreateMap<Set, SetDto>()
                .ForMember(dest => dest.SetId, opt => opt.MapFrom(src => src.Id));

            CreateMap<AddSetCommand, Set>()
                .ConstructUsing(x => new Set(x.Weight, x.NumberOfReps, x.NumberOfSets));
        }
    }
}
