using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformService;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();

            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();

            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(d => d.ExternalId, o => o.MapFrom(s => s.Id));

            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(d => d.ExternalId, o => o.MapFrom(s => s.PlatformId))
                .ForMember(d => d.Commands, o => o.Ignore());
        }
    }
}