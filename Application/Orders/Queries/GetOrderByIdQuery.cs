using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public string Id { get; set; }

        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
        {

            private readonly IMyDbContext _context;
            private readonly IMapper _mapper;

            public GetOrderByIdQueryHandler(IMyDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                return _mapper.Map<OrderDto>(
                    (await _context.Orders.FindAsync(
                        Builders<Order>.Filter.Eq("OrderId", request.Id),
                        null,
                        cancellationToken)).First()
                    );
            }
        }
    }
}
