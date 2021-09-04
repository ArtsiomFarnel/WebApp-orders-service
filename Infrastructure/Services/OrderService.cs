using Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IDatabaseContext context)
        {
            _orders = context.GetCollection<Order>("Orders");
        }

        public List<Order> GetAll() =>
            _orders.Find(o => true).ToList();

        public Order GetOne(string id) =>
            _orders.Find(o => o.Id == id).FirstOrDefault();

        public void Create(Order order) =>
            _orders.InsertOne(order);

        public void Update(string id, Order order) =>
            _orders.ReplaceOne(o => o.Id == id, order);

        public void Remove(string id) =>
            _orders.DeleteOne(o => o.Id == id);
    }
}
