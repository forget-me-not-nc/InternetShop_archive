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
    public class GetLogByIdQuery : IRequest<LogDto>
    {
        public string Id { get; set; }

        public class GetLogByIdQueryHandler : IRequestHandler<GetLogByIdQuery, LogDto>
        {

            private readonly IMyDbContext _context;
            private readonly IMapper _mapper;

            public GetLogByIdQueryHandler(IMyDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LogDto> Handle(GetLogByIdQuery request, CancellationToken cancellationToken)
            {
                return _mapper.Map<LogDto>(
                    (await _context.Logs.FindAsync(
                        Builders<Log>.Filter.Eq("LogId", request.Id),
                        null,
                        cancellationToken)).First(cancellationToken: cancellationToken)
                    );    
            }
        }
    }
}
