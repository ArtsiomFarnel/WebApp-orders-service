using Data.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessageBrokerShared;
using Infrastructure;

namespace Api.Consumers
{
    public class OrderConsumer : IConsumer<OrderFullInfo>
    {
        private readonly IRepositoryManager _repository;

        public OrderConsumer(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<OrderFullInfo> context)
        {
            var message = context.Message;
            await Console.Out.WriteLineAsync(message.Id);
            var function = message.Function;
            var order = MapToOrder(context.Message);
            switch (function)
            {
                case FunctionType.POST:
                    await CreateOrder(order);
                    break;
                case FunctionType.DELETE:
                    DeleteOrder(order);
                    break;
                case FunctionType.PUT:
                    UpdateOrder(order);
                    break;
                case FunctionType.GET:
                    await GetOrder(order);
                    break;
            }
        }

        private async Task CreateOrder(Order order)
        {
            _repository.Orders.Create(order);
            await GetOrder(order);
        }

        private void DeleteOrder(Order orderIn)
        {
            var order = _repository.Orders.GetOne(orderIn.Id);

            if (order == null) return;

            _repository.Orders.Remove(order);
        }

        private void UpdateOrder(Order orderIn)
        {
            var order = _repository.Orders.GetOne(orderIn.Id);

            if (order == null) return;

            _repository.Orders.Update(order);
        }

        private async Task GetOrder(Order orderIn)
        {
            var order = _repository.Orders.GetOne(orderIn.Id);

            if (order != null)
                await Console.Out.WriteLineAsync(order.Id);
            else
                await Console.Out.WriteLineAsync("Error");
        }

        private Order MapToOrder(OrderFullInfo order) =>
            new Order
            {
                Status = "Processed",
                ClientId = order.ClientId,
                ProductId = order.ProductId,
                Id = order.Id
            };
    }
}
