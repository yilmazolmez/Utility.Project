using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Model.Document
{
    public interface IDocument
    {
        [Required]
        [BsonRequired]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        [BsonElement("created_at")]
        public DateTime? created_at { get; set; } 

        [BsonElement("updated_at")]
        [BsonIgnoreIfNull]
        public DateTime? updated_at { get; set; }
    }
}
