using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;

namespace Application.Utils
{
    public static class BsonUtils
    {
        public static BsonDocument Format(dynamic body)
        {
            BsonDocument result;
            string jsonString = JsonConvert.SerializeObject(body);
            jsonString = jsonString.Replace("$", "_$");

            try
            {
                result = BsonDocument.Parse(jsonString);
            }
            catch
            {
                try
                {
                    result = BsonDocument.Parse(JsonConvert.SerializeObject(new { Array = JsonConvert.DeserializeObject<dynamic[]>(jsonString) }));
                }
                catch
                {
                    result = BsonDocument.Parse(JsonConvert.SerializeObject(new { Text = jsonString }));
                }
            }
            return result;
        }
    }
}