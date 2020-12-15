using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Michaelsoft.ContentManager.Server.DatabaseModels
{
    public class DbToken
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Author { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }

        [BsonElement("expireAt")]
        [JsonIgnore]
        public DateTime ExpireAt { get; set; }

    }
}