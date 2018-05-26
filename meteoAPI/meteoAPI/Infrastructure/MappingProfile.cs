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

            //CreateMap<MainDataEntity, MainData>()
            //    .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity));
                //.ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                //Link.To(nameof(Controllers.MainDataController.GetMainDataByIdAsync), new { mainDataId = src.Id })));
            
            //CreateMap<WindEntity, Wind>()
            //    .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree))
            //    .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
            //    Link.To(nameof(Controllers.WindController.GetWindByIdAsync), new { windId = src.Id })));

           

            CreateMap<WeatherRecordEntity, WeatherRecord>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                Link.To(nameof(Controllers.WeatherRecordController.GetWeatherRecordByIdAsync), new { WeatherRecordId = src.Id })))
                .ForMember(dest => dest.CurrentTime, opt => opt.MapFrom(src => src.CurrentTime));
            
            CreateMap<WeatherRecordEntity, MainData>()
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.MainData.Humidity ));
           
            
            CreateMap<WeatherRecordEntity, Wind>()
                 .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Wind.Degree));
                 

               CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(nameof(Controllers.AuthentificationController.GetUserByIdAsync),
                            new { userId = src.Id })));
        }
    }
}
