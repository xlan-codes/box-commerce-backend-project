using Application.Contracts.Persistence;
using Application.Generics.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Generics.Services
{
    public class AgentFLowService : IAgentFlowService
    {
        private readonly IMongoRepository _mongoRepository;
        private readonly ILogger<IMongoRepository> _logger;
        private readonly IMapper _mapper;
        public AgentFLowService(IMongoRepository mongoRepository, IMapper mapper, ILogger<IMongoRepository> logger)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
            _logger = logger;

            try
            {
                _mongoRepository.CreateIndex<AgentFlow>("OfferCode", "_OfferCode_desc");
                _logger.LogInformation("Created OfferCode index");
            }
            catch
            {
                _logger.LogError("MongoDB instance could not create index");
            }
        }

        // public async Task<AgentFlow> CreateOffer(bool isError, CreateOfferCommand request, dynamic response)
        // {
        //     AgentFlow item = new();

        //     item.AgentName = request.UserName;
        //     item.IsError = isError;
        //     item.Error = isError ? "CISLCreateOffer" : null;

        //     item.CNP = request.parties.FirstOrDefault().uniqueId;
        //     item.Mobile = request.parties.FirstOrDefault().mobile;
        //     item.Email = request.parties.FirstOrDefault().email;
        //     item.FirstName = request.parties.FirstOrDefault().firstName;
        //     item.LastName = request.parties.FirstOrDefault().lastName;

        //     item.CISLOfferRequest = BsonUtils.Format(request);
        //     CreateOfferCISLResponse successResponse = null;
        //     if (!isError)
        //     {
        //         successResponse = (CreateOfferCISLResponse)response;
        //     }
        //     item.OfferCode = successResponse?.code;
        //     item.Premium = successResponse?.premium;
        //     item.OfferDate = successResponse?.offerDate;

        //     item.Timestamp = DateTime.UtcNow;
        //     item.CISLOfferResponse = BsonUtils.Format(response);
        //     await _mongoRepository.InsertOneAsync(item);
        //     return item;
        // }

        // public async Task<AgentFlow> UpdateOffer(bool isError, UpdateOfferCommand request, dynamic response)
        // {
        //     bool isNew = false;
        //     AgentFlow item = await _mongoRepository.FindOneAsync<AgentFlow>(x => x.OfferCode == request.code);
        //     if (item == null)
        //     {
        //         isNew = true;
        //         item = new AgentFlow();
        //     }

        //     item.AgentName = request.UserName;
        //     item.IsError = isError;
        //     item.Error = isError ? "CISLUpdateOffer" : null;

        //     item.CNP = request.parties.FirstOrDefault().uniqueId;
        //     item.Mobile = request.parties.FirstOrDefault().mobile;
        //     item.Email = request.parties.FirstOrDefault().email;
        //     item.FirstName = request.parties.FirstOrDefault().firstName;
        //     item.LastName = request.parties.FirstOrDefault().lastName;

        //     item.CISLOfferRequest = BsonUtils.Format(request);
        //     CreateOfferCISLResponse successResponse = null;
        //     if (!isError)
        //     {
        //         successResponse = (CreateOfferCISLResponse)response;
        //     }
        //     item.OfferCode = successResponse?.code;
        //     item.Premium = successResponse?.premium;
        //     item.OfferDate = successResponse?.offerDate;

        //     item.Timestamp = DateTime.UtcNow;
        //     item.CISLOfferResponse = BsonUtils.Format(response);

        //     if (isNew)
        //     {
        //         await _mongoRepository.InsertOneAsync(item);
        //     }
        //     else
        //     {
        //         await _mongoRepository.ReplaceOneAsync(item);
        //     }

        //     return item;
        // }
        // public async Task<AgentFlow> CreateApplication(bool isError, CreateApplicationCommand request, dynamic response)
        // {
        //     var offer = await _mongoRepository.FindOneAsync<AgentFlow>(x => x.OfferCode == request.code);
        //     CreateApplicationCISLResponse successResponse = null;
        //     if (offer != null)
        //     {
        //         offer.IsError = isError;
        //         offer.Error = isError ? "CISLCreateApplication" : null;
        //         offer.CISLCreateApplicationRequest = BsonUtils.Format(request);
        //         if (!isError)
        //         {
        //             successResponse = (CreateApplicationCISLResponse)response;
        //             offer.ApplicationNo = successResponse?.applicationNo;
        //         }

        //         offer.Timestamp = DateTime.UtcNow;
        //         offer.CISLCreateApplicationResponse = BsonUtils.Format(response);
        //         await _mongoRepository.ReplaceOneAsync(offer);

        //         return offer;
        //     }

        //     // Add Main CNP Data from the request
        //     AgentFlow entity = new();
        //     entity.AgentName = request.UserName;
        //     entity.IsError = isError;
        //     entity.Error = isError ? "CISLCreateApplication" : null;

        //     entity.OfferCode = request.code;
        //     entity.CISLCreateApplicationRequest = BsonUtils.Format(request);
        //     if (!isError)
        //     {
        //         successResponse = (CreateApplicationCISLResponse)response;
        //         entity.ApplicationNo = successResponse?.applicationNo;
        //     }

        //     entity.Timestamp = DateTime.UtcNow;
        //     entity.CISLCreateApplicationResponse = BsonUtils.Format(response);

        //     await _mongoRepository.InsertOneAsync(entity);
        //     return entity;
        // }

        // public async Task<AgentFlow> UpdateParty(bool isError, UpdatePartyCommand request, dynamic response)
        // {
        //    var offer = await _mongoRepository.FindOneAsync<AgentFlow>(x => x.OfferCode == request.offer.code);
        //     if (offer != null)
        //     {
        //         offer.IsError = isError;
        //         offer.Error = isError ? "CISLUpdateParty" : null;
        //         offer.CISLUpdatePartyRequest = BsonUtils.Format(request);

        //         offer.Timestamp = DateTime.UtcNow;
        //         offer.CISLCreateApplicationResponse = BsonUtils.Format(response);
        //         await _mongoRepository.ReplaceOneAsync(offer);

        //         return offer;
        //     }

        //     AgentFlow entity = new();
        //     entity.AgentName = request.UserName;
        //     entity.IsError = isError;
        //     entity.Error = isError ? "CISLUpdateParty" : null;

        //     entity.OfferCode = request.offer.code;
        //     entity.CISLUpdatePartyRequest = BsonUtils.Format(request);

        //     entity.Timestamp = DateTime.UtcNow;
        //     entity.CISLCreateApplicationResponse = BsonUtils.Format(response);

        //     await _mongoRepository.InsertOneAsync(entity);
        //     return entity;
        // }
    }
}
