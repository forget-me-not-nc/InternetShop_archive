using Application.Orders.Commands;
using Application.Orders.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ArchivePresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetOrdersListQuery()));
        }

        [HttpGet("{filter}/{value}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll(string filter, string value)
        {
            return Ok(await Mediator.Send(new GetOrdersListQuery()
            {
                Filter = Builders<Order>.Filter.Eq(filter, value)
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery() { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] UpsertOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteOrderCommand() { Id = id }));
        }
    }
}
