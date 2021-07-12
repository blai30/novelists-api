using AutoMapper.Configuration;
using NovelistsApi.Domain.Models;
using NovelistsApi.Infrastructure.Features.Publications;
using NovelistsApi.Infrastructure.Features.Users;

namespace NovelistsApi.Infrastructure.Maps
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Publication, PublicationDto>().ReverseMap();
        }
    }
}
