using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Altkom.UTC.Core.Models.SearchCriteria;
using Altkom.UTC.Core.Service.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // IIdentity

            // IPrincipal

        private readonly ICustomersService customersService;

        private readonly ILogger logger;

        private readonly IHubContext<CustomersHub> hubContext;

        public CustomersController(ILogger<CustomersController> logger, ICustomersService customersService, IHubContext<CustomersHub> hubContext)
        {
            this.logger = logger;
            this.customersService = customersService;
            this.hubContext = hubContext;
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
        //[AllowAnonymous]
        public IActionResult Get([FromQuery] CustomerSearchCriteria criteria)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            logger.LogInformation($"Search by {criteria.FirstName} {criteria.From} {criteria.To}");

            var customers = customersService.Get(criteria);

            return Ok(customers);
        }


        [HttpPost]
        public async Task<IActionResult> Post( Customer customer)
        {
            customersService.Add(customer);

            await hubContext.Clients.All.SendAsync("Added", customer);

            return CreatedAtRoute(new { Id = customer.Id }, customer);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            customersService.Remove(id);

            return Ok();
        }
    }
}
