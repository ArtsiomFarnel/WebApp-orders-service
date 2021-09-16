using Api.Consumers;
using Data;
using GreenPipes;
using Infrastructure;
using Infrastructure.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MessageBrokerShared;

namespace Api
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureControllers(this IServiceCollection services) =>
            services.AddControllers();

        public static void ConfigureMediatR(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetExecutingAssembly());

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IDatabaseContext, DatabaseContext>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders microservice", Version = "v1" });
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConsumer>();
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri(MassTransitConfiguration.Uri), h =>
                    {
                        h.Username(MassTransitConfiguration.UserName);
                        h.Password(MassTransitConfiguration.Password);
                    });
                    cfg.ReceiveEndpoint(MassTransitConfiguration.QueueName, ep =>
                    {
                        ep.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                });

            });

            services.AddMassTransitHostedService();
        }
    }
}
