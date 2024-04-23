using Domain.Attributes;
using Domain.Common;
using MongoDB.Bson;

namespace Domain.Entities
{
    [BsonCollection(nameof(AgentFlow))]
    public class AgentFlow : BaseEntity
    {
        public string AgentName { get; set; }
        public string OfferCode { get; set; } = null;
        public DateTime? OfferDate { get; set; } = null;
        public string CNP { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Premium { get; set; }

        public bool? IsError { get; set; } = null;
        public string Error { get; set; } = null;

        public DateTime Timestamp { get; set; }

        public BsonDocument Request { get; set; } = null;
        public BsonDocument Response { get; set; } = null;

        public string ApplicationNo { get; set; } = null;

        public BsonDocument CISLCreateApplicationRequest { get; set; } = null;
        public BsonDocument CISLCreateApplicationResponse { get; set; } = null;

        public BsonDocument CISLUpdatePartyRequest { get; set; } = null;
        public BsonDocument CISLUpdatePartyResponse { get; set; } = null;
    }

}
