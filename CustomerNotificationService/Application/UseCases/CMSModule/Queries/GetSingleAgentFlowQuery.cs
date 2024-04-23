using Application.Contracts.Persistence;
using Application.Generics.Dtos;
using Application.Generics.Dtos.Settings;
using Application.UseCases.CMSModule.Dtos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;

namespace Application.UseCases.CMSModule.Queries
{
    #region Query
    public class GetSingleAgentFlowQuery : Identity, IRequest<SingleAgentFlowsDto>
    {
        public string Id { get; set; }
    }
    #endregion

    #region Handler

    public class SingleAgentFlowHandler : IRequestHandler<GetSingleAgentFlowQuery, SingleAgentFlowsDto>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository _mongoRepository;
        public SingleAgentFlowHandler(IMapper mapper, IMongoRepository mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }
        public async Task<SingleAgentFlowsDto> Handle(GetSingleAgentFlowQuery request, CancellationToken cancellationToken)
        {
            var result = await _mongoRepository.FindOneAsync<AgentFlow>(x => x.Id == new ObjectId(request.Id));

            return _mapper.Map<AgentFlow, SingleAgentFlowsDto>(result);
        }
    }
    #endregion
}
