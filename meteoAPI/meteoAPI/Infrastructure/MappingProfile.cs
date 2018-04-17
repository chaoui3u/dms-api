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
                Link.To(nameof(Controllers.SitesController.GetSitesByIdAsync), new { siteId = src.Id })));

            CreateMap<MesureEntity, Mesure>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.MesuresController.GetMesuresByIdAsync), new { mesureId = src.Id })));

            CreateMap<MainDataEntity, MainData>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.MainDataController.GetMainDataByIdAsync), new { mainDataId = src.Id })));

            CreateMap<RainEntity, Rain>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.RainController.GetRainByIdAsync), new { rainId = src.Id })));

            CreateMap<SnowEntity, Snow>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.SnowController.GetSnowByIdAsync), new { snowId = src.Id })));

            CreateMap<SunEntity, Sun>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.SunController.GetSunByIdAsync), new { sunId = src.Id })));

            CreateMap<WeatherEntity, Weather>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WeatherController.GetWeatherByIdAsync), new { weatherId = src.Id })));

            CreateMap<WeatherHistoryEntity, WeatherHistory>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WeatherHistoryController.GetWeatherHistoryByIdAsync), new { weatherHistoryId = src.Id })));

            CreateMap<WindEntity, Wind>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WindController.GetWindByIdAsync), new { windId = src.Id })));

            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.AuthentificationController.GetUserByIdAsync),
                            new { userId = src.Id })));
        }
    }
}
