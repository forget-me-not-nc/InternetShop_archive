using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IMyDbContext
    {
        IMongoCollection<Log> Logs { get; }
        IMongoCollection<Order> Orders { get; }
    }
}
