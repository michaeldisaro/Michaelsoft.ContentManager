using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Michaelsoft.ContentManager.Server.DatabaseModels
{
    public class DbContent
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Type { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Locale { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string HtmlContent { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string TextContent { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Updated { get; set; }

        public List<string> Owners { get; set; } = new List<string>();

        public List<string> Tags { get; set; } = new List<string>();

    }
}