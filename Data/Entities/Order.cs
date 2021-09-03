using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Order : IEntityBase
    {
        [BsonId]
        public string Id { get; set; }
        public string ClientId { get; set; }
        public int ProductId { get; set; }
    }
}
