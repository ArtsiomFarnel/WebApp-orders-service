using Data.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public OrdersController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Order>> Get() =>
            _repository.Orders.GetAll();

        [HttpGet("{id}", Name = "GetOrder")]
        public ActionResult<Order> Get(string id)
        {
            var order = _repository.Orders.GetOne(id);

            if (order == null) return NotFound();

            return order;
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _repository.Orders.Create(order);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Order orderIn)
        {
            var order = _repository.Orders.GetOne(orderIn.Id);

            if (order == null) return NotFound();

            _repository.Orders.Update(orderIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Order orderIn)
        {
            var order = _repository.Orders.GetOne(orderIn.Id);

            if (order == null) return NotFound();

            _repository.Orders.Remove(order);

            return NoContent();
        }
    }
}
