using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands
{
    public class UpsertOrderCommand : IRequest<string>
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public ICollection<Book> Books { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }

        public class UpsertOrderCommandHandler : IRequestHandler<UpsertOrderCommand, string>
        {
            private readonly IMyDbContext _context;

            public UpsertOrderCommandHandler(IMyDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpsertOrderCommand request, CancellationToken cancellationToken)
            {
                Order order;

                if(!string.IsNullOrWhiteSpace(request.Id))
                {
                    order = await _context.Orders
                        .FindAsync(
                            Builders<Order>.Filter.Eq("OrderId", request.Id),
                            null,
                            cancellationToken)
                        .Result
                        .FirstAsync();
                }
                else
                {
                    order = new Order() { OrderId = ObjectId.GenerateNewId().ToString() };
                }

                order.Address = Domain.ValueObjects.Address.For(request.Address);
                order.ClientId = request.ClientId;
                order.Books = request.Books;
                order.Date = DateTime.ParseExact(request.Date, Settings.GetDateFormat(), Settings.GetDateProvider());
                order.TotalPrice = request.Books.Select(el => el.Price).Sum();

                if(!string.IsNullOrWhiteSpace(request.Id)) await _context.Orders.ReplaceOneAsync(
                    filter: Builders<Order>.Filter.Eq("OrderId", request.Id),
                    replacement: order,
                    options: null as ReplaceOptions,
                    cancellationToken: cancellationToken);

                else await _context.Orders.InsertOneAsync(order, null, cancellationToken);

                return order.OrderId;
            }
        }
    }
}
