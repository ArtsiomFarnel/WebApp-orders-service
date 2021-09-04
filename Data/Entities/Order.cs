using Data.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    [BsonCollection("Orders")]
    public class Order : EntityBase
    {
        public string ClientId { get; set; }
        public int ProductId { get; set; }
    }
}
