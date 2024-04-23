using Domain.Common;
using MongoDB.Bson.Serialization;

namespace Infrastructure.EntityConfigurations.Modules
{
    public class BaseEntityConfiguration
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(map =>
            {
                map.AutoMap();
            });
        }
    }
}
