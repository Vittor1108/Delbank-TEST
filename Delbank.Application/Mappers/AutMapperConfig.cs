using AutoMapper;

namespace Delbank.Application.Mappers
{
    public class AutMapperConfig
    {
        public static IMapper ConfigureAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<DirectorMapper>();
                config.AddProfile<DvdMapper>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}
