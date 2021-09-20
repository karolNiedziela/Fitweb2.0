using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Information.Name))
                .ForMember(dest => dest.PartOfBody, opt => opt.MapFrom(src => src.PartOfBody.GetDisplayName()));
        }
    }
}
