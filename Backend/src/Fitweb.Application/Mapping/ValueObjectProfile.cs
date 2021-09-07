using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class ValueObjectProfile : Profile
    {
        public ValueObjectProfile()
        {
            CreateMap<InformationDto, Information>().ConvertUsing(src =>
                Information.Create(src.Name, src.Description));

            CreateMap<NutrientDto, Nutrient>().ConvertUsing(src =>
                Nutrient.Create(src.Protein, src.Carbohydrate, src.Fat, src.SaturatedFat, src.Sugar, src.Fiber,
                    src.Salt));

            CreateMap<double, Calories>().ConvertUsing(src => Calories.Create(src));

            CreateMap<Calories, double>().ConvertUsing(x => x.Value);
        }
    }
}
