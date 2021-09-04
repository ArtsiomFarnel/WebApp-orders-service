using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public interface IRepositoryManager
    {
        public IOrderRepository Orders { get; }
    }

    public class RepositoryManager : IRepositoryManager
    {
        private readonly IDatabaseContext _context;
        private IOrderRepository _orderRepository;

        public RepositoryManager(IDatabaseContext context)
        {
            _context = context;
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }

    }
}
