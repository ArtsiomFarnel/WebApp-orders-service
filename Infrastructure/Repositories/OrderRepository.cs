using Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {

    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDatabaseContext context) : base(context) { }
    }
}
