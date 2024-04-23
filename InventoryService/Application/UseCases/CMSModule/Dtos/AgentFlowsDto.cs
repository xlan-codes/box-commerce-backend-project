using MongoDB.Bson;

namespace Application.UseCases.CMSModule.Dtos
{
    public class AgentFlowsDto
    {
        public ObjectId Id { get; set; }
        public string OfferCode { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsError { get; set; }
        public string Error { get; set; }
        public string CNP { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfferAgentName { get; set; }
        public string OldPolicyNo { get; set; }
        public string PolicyAgentName { get; set; }
    }


}
