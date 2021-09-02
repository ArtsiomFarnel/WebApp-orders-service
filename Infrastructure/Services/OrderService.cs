using Data.Entities;
using Data.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IOrderDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public List<Order> Get() =>
            _orders.Find(book => true).ToList();

        public Order Get(string id) =>
            _orders.Find(o => o.Id == id).FirstOrDefault();

        public Order Create(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Order order) =>
            _orders.ReplaceOne(o => o.Id == id, order);

        public void Remove(Order order) =>
            _orders.DeleteOne(o => o.Id == order.Id);

        public void Remove(string id) =>
            _orders.DeleteOne(o => o.Id == id);
    }
}
