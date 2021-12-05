using ApplicationCore.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderPublisher.Abstract;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderPublisher.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        IOrderRepository _repository;
        ILogger<OrderController> _logger;
        IProducerWrapper _producer;

        public OrderController(IOrderRepository repository, IProducerWrapper producer, ILogger<OrderController> logger)
        {
            _repository = repository;
            _producer = producer;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> PostAsync(Order order)
        {
            if (order== null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _repository.Create(order);

            //send order details to message queue
            _producer.WriteMessageAsync(JsonSerializer.Serialize(order));

            return Ok(order) ;
        }
    }
}
