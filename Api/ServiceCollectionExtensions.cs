using Data.Settings;
using Infrastructure.Services;
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
            // Order Collection
            services.Configure<OrderDatabaseSettings>(
                configuration.GetSection(nameof(OrderDatabaseSettings)));

            services.AddSingleton<IOrderDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<OrderDatabaseSettings>>().Value);

            services.AddSingleton<OrderService>();
            //
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }
    }
}
