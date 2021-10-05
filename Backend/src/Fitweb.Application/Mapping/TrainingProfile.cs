using AutoMapper;
using Fitweb.Application.Commands.Trainings.Add;
using Fitweb.Application.Commands.Trainings.Update;
using Fitweb.Application.DTO;
using Fitweb.Application.Mapping.Converters;
using Fitweb.Application.Mapping.Resolvers;
using Fitweb.Domain.Extensions;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<Training, TrainingDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Information.Name))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.GetDisplayName()));

            CreateMap<TrainingExercise, ExerciseDto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExerciseId))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Exercise.Information.Name))
             .ForMember(dest => dest.PartOfBody, opt => opt.MapFrom(src => src.Exercise.PartOfBody.GetDisplayName()));
            CreateMap<Training, TrainingExercisesDto>()
                .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises));

            CreateMap<TrainingExercise, TrainingExerciseWithSetsDto>()
                .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.ExerciseId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Exercise.Information.Name))
                .ForMember(dest => dest.Sets, opt => opt.MapFrom(src => src.Sets));
            CreateMap<Training, TrainingExercisesWithSetsDto>()
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises))
                .ForMember(dest => dest.TrainingId, opt => opt.MapFrom(src => src.Id));


        }
    }
}
