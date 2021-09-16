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
        public void Update(TEntity entity);
        public void Remove(TEntity entity);
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

        public void Update(TEntity entity) =>
            _collection.ReplaceOne(o => o.Id == entity.Id, entity);

        public void Remove(TEntity entity) =>
            _collection.DeleteOne(o => o.Id == entity.Id);
    }
}
