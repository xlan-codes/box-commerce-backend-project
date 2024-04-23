﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public MongoDB.Bson.ObjectId Id { get; set; }

        [BsonRequired]
        public DateTime CreatedAt => Id.CreationTime;
    }
}
