using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.FoodProducts.Commands;
using Fitweb.Application.FoodProducts.Queries.GetFoodProductsList;
using Fitweb.Domain.Extensions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class FoodProductProfile : Profile
    { 
        public FoodProductProfile()
        {
            CreateMap<AddFoodProductCommand, FoodProduct>();

            CreateMap<FoodProduct, FoodProductDto>()
                .ForMember(dest => dest.Calories, src => src.MapFrom(x => x.Calories.Value))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Information.Name))
                .ForMember(dest => dest.Protein, src => src.MapFrom(x => x.Nutrient.Protein))
                .ForMember(dest => dest.Carbohydrate, src => src.MapFrom(x => x.Nutrient.Carbohydrate))
                .ForMember(dest => dest.Fat, src => src.MapFrom(x => x.Nutrient.Fat))
                .ForMember(dest => dest.Group, src => src.MapFrom(x => x.Group.GetDisplayName()));

            CreateMap<FoodProduct, FoodProductDetailsDto>()
                .ForMember(dest => dest.Fiber, src => src.MapFrom(x => x.Nutrient.Fiber))
                .ForMember(dest => dest.Salt, src => src.MapFrom(x => x.Nutrient.Salt))
                .ForMember(dest => dest.Sugar, src => src.MapFrom(x => x.Nutrient.Sugar))
                .ForMember(dest => dest.SaturatedFat, src => src.MapFrom(x => x.Nutrient.SaturatedFat))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Information.Description))
               .IncludeBase<FoodProduct, FoodProductDto>();

        }
    }
}
