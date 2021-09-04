using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public interface IEntityBase
    {
        [BsonId]
        public string Id { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        public string Id { get; set; }
    }
}
