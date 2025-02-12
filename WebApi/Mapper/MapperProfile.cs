using AutoMapper;
using WebApi.Domain;

namespace WebApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<WeatherForecastDto, WeatherForecast>();
        }

    }
}
