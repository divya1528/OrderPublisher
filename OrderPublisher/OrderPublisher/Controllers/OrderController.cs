using ApplicationCore.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderPublisher.Abstract;
using System;
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

            try
            {
                string orderString = JsonSerializer.Serialize(order);
                _logger.LogInformation("Saving order details in database {0}", orderString);
                await _repository.Create(order);

                //send order details to message queue
                _logger.LogInformation("pushing order details to message queue: {0}", orderString);
                _producer.WriteMessageAsync(orderString);

                return Ok(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
