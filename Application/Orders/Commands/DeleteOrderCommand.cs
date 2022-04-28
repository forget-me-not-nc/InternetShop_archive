using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
        {
            private readonly IMyDbContext _context;

            public DeleteOrderCommandHandler(IMyDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var result = await _context.Orders.DeleteOneAsync(Builders<Order>.Filter.Eq("OrderId", request.Id), cancellationToken);

                return Convert.ToBoolean(result.DeletedCount);
            }
        }
    }
}
