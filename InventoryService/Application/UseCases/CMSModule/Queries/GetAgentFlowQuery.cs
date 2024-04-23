using Application.Contracts.Persistence;
using Application.Generics.Dtos;
using Application.Generics.Dtos.ResponseDtos;
using Application.UseCases.CMSModule.Dtos;
using Application.UseCases.CMSModule.Enums;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace Application.UseCases.CMSModule.Queries
{
    #region Query
    public class GetAgentFlowQuery : Identity, IRequest<PaginationDto<IEnumerable<AgentFlowsDto>>>
    {
        public int PageSize { get; set; } = 1;
        public int PageIndex { get; set; } = 1;
        public string SearchTerm { get; set; }
        public AgentFlowColumnEnum SortByColumn { get; set; } = AgentFlowColumnEnum.Timestamp;
        public bool SortDescending { get; set; }
    }
    #endregion

    #region Handler

    public class GetAgentFlowHandler : IRequestHandler<GetAgentFlowQuery, PaginationDto<IEnumerable<AgentFlowsDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository _mongoRepository;
        public GetAgentFlowHandler(IMapper mapper, IMongoRepository mongoRepository)
        {
            _mapper = mapper;
            _mongoRepository = mongoRepository;
        }
        public async Task<PaginationDto<IEnumerable<AgentFlowsDto>>> Handle(GetAgentFlowQuery request, CancellationToken cancellationToken)
        {
            var query = _mongoRepository.AsQueryable<AgentFlow>();
            var response = new PaginationDto<IEnumerable<AgentFlowsDto>>();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(p =>
                    (p.AgentName).ToLower().Contains(request.SearchTerm));
            }

            // Sort by specified column
            query = request.SortByColumn switch
            {
                AgentFlowColumnEnum.LastName => request.SortDescending ? query.OrderByDescending(d => d.AgentName) : query.OrderBy(d => d.AgentName),
                _ => request.SortDescending ? query.OrderByDescending(d => d.Timestamp) : query.OrderBy(d => d.Timestamp),
            };
            var result = await Task.Run(() =>  query.Skip((request.PageIndex - 1) * request.PageSize)
                          .Take(request.PageSize)
                          .ToList());

            var totalCount = query.Count();
            response.TotalRecords = totalCount;
            response.TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize); ;
            response.Data = _mapper.Map<IEnumerable<AgentFlow>, IEnumerable<AgentFlowsDto>>(result);

            return response;

        }
    }
    #endregion
}
