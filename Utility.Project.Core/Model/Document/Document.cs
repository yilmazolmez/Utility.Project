using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Model.Document
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }

        [BsonElement("created_at")]
        [BsonIgnoreIfNull] 
        public DateTime? created_at { get; set; } = null;

        [BsonElement("updated_at")]
        [BsonIgnoreIfNull]
        public DateTime? updated_at { get; set; } = null;
    }
}
