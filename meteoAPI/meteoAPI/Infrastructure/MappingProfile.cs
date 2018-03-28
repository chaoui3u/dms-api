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
        }
    }
}
