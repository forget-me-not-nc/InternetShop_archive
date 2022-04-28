using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyDbContext
{
    public class MyMongoDbContext : IMyDbContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public MyMongoDbContext()
        {
            _client = new MongoClient(DatabaseSettings.ConnectionString);
            _database = _client.GetDatabase(DatabaseSettings.DatabaseName);
        }

        public IMongoCollection<Log> Logs => _database.GetCollection<Log>(DatabaseSettings.Logs);

        public IMongoCollection<Order> Orders => _database.GetCollection<Order>(DatabaseSettings.OrderingHistory);
    }
}
