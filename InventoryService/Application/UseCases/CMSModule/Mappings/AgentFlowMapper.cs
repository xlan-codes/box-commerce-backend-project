using Application.UseCases.CMSModule.Dtos;
using AutoMapper;
using Domain.Entity;

namespace Application.UseCases.CMSModule.Mappings
{
    public class AgentFlowMapping : Profile
    {
        public AgentFlowMapping()
        {
            CreateMap<AgentFlow, AgentFlowsDto>();
        }
    }
}
