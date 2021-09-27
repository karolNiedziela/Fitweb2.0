using AutoMapper;
using Fitweb.Application.Commands.FoodProducts.Add;
using Fitweb.Application.DTO;
using Fitweb.Application.Mapping.Resolvers;
using Fitweb.Domain.Extensions;
using Fitweb.Domain.FoodProducts;
using System;

namespace Fitweb.Application.Mapping
{
    public class FoodProductProfile : Profile
    { 
        public FoodProductProfile()
        {
            CreateMap<AddFoodProductCommand, FoodProduct>()
                .ForMember(dest => dest.Information,
                opt => opt.MapFrom<InformationResolver<AddFoodProductCommand, FoodProduct>>())
                .ForMember(dest => dest.Nutrient, opt => opt.MapFrom<NutrientResolver>())
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => Enum.GetName(typeof(FoodGroup), src.FoodGroupId)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<FoodProduct, FoodProductDto>()
                .ForMember(dest => dest.Calories, opt => opt.MapFrom(src => src.Calories.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Information.Name))
                .ForMember(dest => dest.Protein, opt => opt.MapFrom(src => src.Nutrient.Protein))
                .ForMember(dest => dest.Carbohydrate, opt => opt.MapFrom(src => src.Nutrient.Carbohydrate))
                .ForMember(dest => dest.Fat, opt => opt.MapFrom(src => src.Nutrient.Fat))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group.GetDisplayName()));

            CreateMap<FoodProduct, FoodProductDetailsDto>()
                .ForMember(dest => dest.Fiber, opt => opt.MapFrom(src => src.Nutrient.Fiber))
                .ForMember(dest => dest.Salt, opt => opt.MapFrom(src => src.Nutrient.Salt))
                .ForMember(dest => dest.Sugar, opt => opt.MapFrom(src => src.Nutrient.Sugar))
                .ForMember(dest => dest.SaturatedFat, opt => opt.MapFrom(src => src.Nutrient.SaturatedFat))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Information.Description))
               .IncludeBase<FoodProduct, FoodProductDto>();
        }
    }
}
