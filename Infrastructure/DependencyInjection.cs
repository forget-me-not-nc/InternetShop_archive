using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.MyDbContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IMyDbContext, MyMongoDbContext>();

            BsonClassMap.RegisterClassMap<Log>(log =>
            {
                log.AutoMap();
                log.MapIdMember(m => m.LogId);
            });

            BsonClassMap.RegisterClassMap<Order>(order =>
            {
                order.AutoMap();
                order.MapIdMember(m => m.OrderId);
            });

            return services;
        }
    }
}
