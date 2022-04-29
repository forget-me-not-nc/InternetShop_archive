using Application.Common;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logs.Commands
{
    public class UpsertLogCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }

        public class UpsertLogCommandHandler : IRequestHandler<UpsertLogCommand, string>
        {
            private readonly IMyDbContext _context;
         
            public UpsertLogCommandHandler(IMyDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpsertLogCommand request, CancellationToken cancellationToken)
            {
                Log log;

                if (!string.IsNullOrWhiteSpace(request.Id))
                {
                    log = await _context.Logs.FindAsync(
                        Builders<Log>.Filter.Eq("LogId", request.Id),
                        null, 
                        cancellationToken)
                        .Result
                        .FirstAsync(cancellationToken: cancellationToken);
                }
                else
                {
                    log = new Log
                    {
                        LogId = ObjectId.GenerateNewId().ToString()
                    };
                }

                log.Date = DateTime.ParseExact(request.Date, Settings.GetDateFormat(), Settings.GetDateProvider());
                log.Message = request.Message;
                log.Type = Enum.Parse<LogTypes>(request.Type);

                if (!string.IsNullOrWhiteSpace(request.Id)) await _context.Logs.ReplaceOneAsync(
                    filter: Builders<Log>.Filter.Eq("LogId", request.Id), 
                    replacement: log,
                    options: null as ReplaceOptions,
                    cancellationToken: cancellationToken);

                else await _context.Logs.InsertOneAsync(log, null, cancellationToken);

                return log.LogId;
            }
        }
    }
}
