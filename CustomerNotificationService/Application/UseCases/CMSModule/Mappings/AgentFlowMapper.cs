using Application.UseCases.CMSModule.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.CMSModule.Mappings
{
    public class AgentFlowMapping : Profile
    {
        public AgentFlowMapping()
        {
            CreateMap<AgentFlow, AgentFlowsDto>();
            CreateMap<AgentFlow, SingleAgentFlowsDto>()
                .ForMember(dest => dest.Request, opt => opt.MapFrom(src => src.Request.ToString()))
                .ForMember(dest => dest.Response, opt => opt.MapFrom(src => src.Response.ToString()));
        }
    }
}
