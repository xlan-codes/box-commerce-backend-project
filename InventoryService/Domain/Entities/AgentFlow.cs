using Domain.Attributes;
using Domain.Common;
using MongoDB.Bson;

namespace Domain.Entity
{
    [BsonCollection(nameof(AgentFlow))]
    public class AgentFlow : BaseEntity
    {
        public string AgentName { get; set; }

        public bool? IsError { get; set; } = null;
        public string Error { get; set; } = null;

        public DateTime Timestamp { get; set; }
    }

}
