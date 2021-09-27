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
            CreateMap<double, Calories>().ConvertUsing(src => Calories.Create(src));

            CreateMap<Calories, double>().ConvertUsing(calories => calories.Value);
        }
    }
}
