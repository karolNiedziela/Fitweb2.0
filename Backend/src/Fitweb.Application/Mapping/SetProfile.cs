using AutoMapper;
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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
