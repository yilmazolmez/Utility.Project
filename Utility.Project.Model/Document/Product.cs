using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Model.Document
{
    public class Product : Utility.Project.Core.Model.Document.Document
    {
        [BsonElement("product_code")]
        public string product_code { get; set; }

        [BsonElement("product_name")]
        public string product_name { get; set; }
    }
}
