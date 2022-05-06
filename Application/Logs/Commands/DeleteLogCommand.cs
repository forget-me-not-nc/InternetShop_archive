using Application.Common.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace Application.Logs.Commands
{
    public class DeleteLogCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public class DeleteLogCommandHandler : IRequestHandler<DeleteLogCommand, bool>
        {
            private readonly IMyDbContext _context;
         
            public DeleteLogCommandHandler(IMyDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteLogCommand request, CancellationToken cancellationToken)
            {
                var result = await _context.Logs.DeleteOneAsync(el => el.LogId == request.Id, cancellationToken);

                return Convert.ToBoolean(result.DeletedCount);
            }
        }
    }
}
