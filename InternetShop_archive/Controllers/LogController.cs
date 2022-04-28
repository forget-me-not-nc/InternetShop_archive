using Application.Logs.Commands;
using Application.Logs.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ArchivePresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetLogsListQuery()));
        }

        [HttpGet("{filter}/{value}")]
        public async Task<ActionResult<IEnumerable<LogDto>>> GetAll(string filter, string value)
        {
            return Ok(await Mediator.Send(new GetLogsListQuery()
            {
                Filter = Builders<Log>.Filter.Eq(filter, value)
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LogDto>>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetLogByIdQuery() { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] UpsertLogCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteLogCommand() { Id = id } ));
        }
    }
}
