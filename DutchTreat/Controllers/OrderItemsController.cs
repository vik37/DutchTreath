using AutoMapper;
using Dutch.Data;
using Dutch.Data.Entities;
using Dutch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dutch.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public OrderItemsController(IDutchRepository repo, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repo.GetOrderById(orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repo.GetOrderById(orderId);
            if(order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null) return Ok(_mapper.Map<OrderItem,OrderItemViewModel>(item));
            }
            return NotFound();
        }
    }
}
