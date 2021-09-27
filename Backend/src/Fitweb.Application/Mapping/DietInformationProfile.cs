using AutoMapper;
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
            CreateMap<UpdateDietInformationCommand, DietInformation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DietInformationId));
        }
    }
}
