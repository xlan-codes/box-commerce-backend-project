using Infrastructure.EntityConfigurations.Modules;
using Infrastructure.EntityConfigurations.Modules.Common;
using MongoDB.Bson.Serialization.Conventions;

namespace Infrastructure.EntityConfigurations
{
    public class MongoDBSchemaConfiguration
    {
        public static void Configure()
        {
            var conventions = new ConventionPack();
            conventions.Add(new IgnoreIfNullConvention(true));
            conventions.Add(new IgnoreExtraElementsConvention(true));
            ConventionRegistry.Register("IgnoreIfNull", conventions, x => true);

            BaseEntityConfiguration.Configure();
        }
    }
}
