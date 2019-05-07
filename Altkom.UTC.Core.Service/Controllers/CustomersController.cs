using Altkom.UTC.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = customersService.Get();

            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var customer = customersService.Get(id);

            if (customer == null)
                return NotFound();


            return Ok(customer);
        }
    }
}
