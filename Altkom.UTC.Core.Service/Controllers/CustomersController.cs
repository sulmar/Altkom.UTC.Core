using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        private readonly ILogger logger;

        public CustomersController(ILogger<CustomersController> logger, ICustomersService customersService)
        {
            this.logger = logger;
            this.customersService = customersService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = customersService.Get();

        //    return Ok(customers);
        //}

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = customersService.Get(id);

            if (customer == null)
                return NotFound();


            return Ok(customer);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] CustomerSearchCriteria criteria)
        {
            logger.LogInformation($"Search by {criteria.FirstName} {criteria.From} {criteria.To}");

            var customers = customersService.Get(criteria);

            return Ok(customers);
        }


        [HttpPost]
        public IActionResult Post( Customer customer)
        {
            customersService.Add(customer);

            return CreatedAtRoute(new { Id = customer.Id }, customer);

        }
    }
}
