using Dutch.Data;
using Dutch.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dutch.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IDutchRepository repo, ILogger<ProductsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_repo.GetAllProducts());
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get produsts: {ex}");
                return BadRequest("Bad Request");
            }
            
        }
    }
}
