using AutoMapper;
using Fitweb.Application.Commands.DietInformations.Add;
using Fitweb.Application.Commands.DietInformations.Update;
using Fitweb.Domain.Athletes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class DietInformationProfile : Profile
    {
        public DietInformationProfile()
        {
            CreateMap<AddDietInformationCommand, DietInformation>()
                .ConstructUsing(src => new DietInformation(src.TotalCalories, src.TotalProteins, src.TotalCarbohydrates,
                src.TotalFats, src.StartDate, src.EndDate));

            // TODO: Rethink it (remove this map)
            CreateMap<UpdateDietInformationCommand, DietInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DietInformationId));
        }
    }
}
