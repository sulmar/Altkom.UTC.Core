using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Controllers
{

    [Route("api/[controller]")]
    [Route("api/urzadzenia")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesService devicesService;

        public DevicesController(IDevicesService devicesService)
        {
            this.devicesService = devicesService;
        }

        //[HttpGet]
        //public IActionResult Pobierz()
        //{
        //    var devices = devicesService.Get();

        //    return Ok(devices);
        //}

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var device = devicesService.Get(id);

            if (device == null)
                return NotFound();


            return Ok(device);
        }


        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            Device device = devicesService.Get(name);

            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [Route("~/api/v1/customers/{customerId}/devices")]
        [HttpGet]
        public IActionResult GetByCustomerV1(int customerId)
        {
            throw new NotImplementedException();
        }

        [Route("~/api/customers/{customerId}/devices")]
        [HttpGet]
        public IActionResult GetByCustomerV2(int customerId)
        {

            var request = this.Request;

            throw new NotImplementedException();
        }



        [HttpGet()]
        public IActionResult Get([FromQuery] string color, [FromQuery] bool isActive)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            devicesService.Remove(id);

            return Ok();
        }


        [HttpPost]
        //public IActionResult Post([FromBody] Device device
        public IActionResult Post(Device device)
        {
            devicesService.Add(device);

            return CreatedAtRoute(new { device.Id }, device);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Device device)
        {
            if (device.Id != id)
                return BadRequest();

            devicesService.Update(device);

            return Ok();
        }
    }
}
