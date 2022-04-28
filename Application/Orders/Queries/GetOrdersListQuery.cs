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
    public class GetOrdersListQuery : IRequest<List<OrderDto>>
    {
        public FilterDefinition<Order>? Filter { get; set; }

        public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDto>>
        {

            private readonly IMyDbContext _context;
            private readonly IMapper _mapper;

            public GetOrdersListQueryHandler(IMyDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
            {
                var resultList = new List<Order>();

                if (request.Filter is null) resultList = await _context.Orders.FindAsync(_ => true, null, cancellationToken).Result.ToListAsync();
                else resultList = await _context.Orders.FindAsync(request.Filter, null, cancellationToken).Result.ToListAsync(cancellationToken: cancellationToken);

                return resultList.Select(order => _mapper.Map<OrderDto>(order)).ToList();
            }
        }
    }
}
