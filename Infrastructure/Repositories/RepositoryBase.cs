using Data.Attributes;
using Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : IEntityBase
    {
        public List<TEntity> GetAll();
        public TEntity GetOne(string id);
        public void Create(TEntity entity);
        public void Update(string id, TEntity entity);
        public void Remove(string id);
    }

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : IEntityBase
    {
        private readonly IMongoCollection<TEntity> _collection;

        public RepositoryBase(IDatabaseContext context)
        {
            _collection = context.GetCollection<TEntity>(GetCollectionName());
        }

        private static string GetCollectionName()
        {
            return (typeof(TEntity)
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute)
                .CollectionName;
        }

        public List<TEntity> GetAll() =>
            _collection.Find(o => true).ToList();

        public TEntity GetOne(string id) =>
            _collection.Find(o => o.Id == id).FirstOrDefault();

        public void Create(TEntity entity) =>
            _collection.InsertOne(entity);

        public void Update(string id, TEntity entity) =>
            _collection.ReplaceOne(o => o.Id == id, entity);

        public void Remove(string id) =>
            _collection.DeleteOne(o => o.Id == id);
    }
}
