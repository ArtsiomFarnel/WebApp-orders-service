using Data.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessageBrokerShared;

namespace Application.Services
{
    public class OrderConsumer : IConsumer<OrderFullInfo>
    {
        public async Task Consume(ConsumeContext<OrderFullInfo> context)
        {
            var data = context.Message;
            await Console.Out.WriteLineAsync(data.ClientId);
        }
    }
}
