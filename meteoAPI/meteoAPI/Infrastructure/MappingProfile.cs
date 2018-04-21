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
           
            CreateMap<MainDataEntity, MainData>()
                .ForMember(dest => dest.Humidity , opt => opt.MapFrom(src => src.Humidity))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.MainDataController.GetMainDataByIdAsync), new { mainDataId = src.Id })));

            CreateMap<RainEntity, Rain>()
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.RainController.GetRainByIdAsync), new { rainId = src.Id })));

            CreateMap<SnowEntity, Snow>()
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.Volume))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.SnowController.GetSnowByIdAsync), new { snowId = src.Id })));

            CreateMap<SunEntity, Sun>()
                .ForMember(dest => dest.SunRise, opt => opt.MapFrom(src => src.SunRise))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.SunController.GetSunByIdAsync), new { sunId = src.Id })));

            CreateMap<WeatherEntity, Weather>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WeatherController.GetWeatherByIdAsync), new { weatherId = src.Id })));

          

            CreateMap<WindEntity, Wind>()
                .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WindController.GetWindByIdAsync), new { windId = src.Id })));

            CreateMap<CloudsEntity, Clouds>()
                .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.All))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.CloudsController.GetCloudsByIdAsync), new { cloudsId = src.Id })));

            CreateMap<WeatherRecordEntity, WeatherRecord>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WeatherRecordController.GetWeatherRecordByIdAsync), new { WeatherRecordId = src.Id })))
                .ForMember(dest => dest.Clouds, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.CloudsController.GetCloudsByIdAsync), new { cloudsId = src.Clouds.Id })))
                .ForMember(dest => dest.MainData, opt => opt.MapFrom(src =>
               Link.To(nameof(Controllers.MainDataController.GetMainDataByIdAsync), new { mainDataId = src.MainData.Id })))
                 .ForMember(dest => dest.Rain, opt => opt.MapFrom(src =>
               Link.To(nameof(Controllers.RainController.GetRainByIdAsync), new { RainId = src.Rain.Id })))
                .ForMember(dest => dest.Snow, opt => opt.MapFrom(src =>
               Link.To(nameof(Controllers.SnowController.GetSnowByIdAsync), new { SnowId = src.Snow.Id })))
                .ForMember(dest => dest.Sun, opt => opt.MapFrom(src =>
               Link.To(nameof(Controllers.SunController.GetSunByIdAsync), new { SunId = src.Sun.Id })))
                .ForMember(dest => dest.Weather, opt => opt.MapFrom(src =>
               Link.To(nameof(Controllers.WeatherController.GetWeatherByIdAsync), new { WeatherId = src.Weather.Id })))
                 .ForMember(dest => dest.Wind, opt => opt.MapFrom(src =>
               Link.To(nameof(Controllers.WindController.GetWindByIdAsync), new { WindId = src.Wind.Id })))
                 .ForMember(dest => dest.CurrentTime, opt => opt.MapFrom(src => src.CurrentTime));

               CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.AuthentificationController.GetUserByIdAsync),
                            new { userId = src.Id })));
        }
    }
}
