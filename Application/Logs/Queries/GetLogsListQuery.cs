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

namespace Application.Logs.Queries
{
    public class GetLogsListQuery : IRequest<List<LogDto>>
    {
        public FilterDefinition<Log>? Filter { get; set; }

        public class GetLogsListQueryHandler : IRequestHandler<GetLogsListQuery, List<LogDto>>
        {

            private readonly IMyDbContext _context;
            private readonly IMapper _mapper;

            public GetLogsListQueryHandler(IMyDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<LogDto>> Handle(GetLogsListQuery request, CancellationToken cancellationToken)
            {
                var resultList = new List<Log>();

                if (request.Filter is null) resultList = await _context.Logs.FindAsync(_ => true, null, cancellationToken).Result.ToListAsync(); 
                else resultList = await _context.Logs.FindAsync(request.Filter, null, cancellationToken).Result.ToListAsync(cancellationToken: cancellationToken);

                return resultList.Select(log => _mapper.Map<LogDto>(log)).ToList();
            }
        }
    }
}
