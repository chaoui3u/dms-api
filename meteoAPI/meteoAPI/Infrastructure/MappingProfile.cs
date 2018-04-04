using AutoMapper;
using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SiteEntity, Site>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.RestrictedController.GetSitesByIdAsync), new { siteId = src.Id })));

            CreateMap<MesureEntity, Mesure>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.RestrictedController.GetMesuresByIdAsync), new { mesureId = src.Id })));

            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.AuthentificationController.GetUserByIdAsync),
                            new { userId = src.Id })));
        }
    }
}
