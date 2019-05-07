using Altkom.UTC.Core.FakeServices.Fakers;
using Altkom.UTC.Core.IServices;
using Altkom.UTC.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.UTC.Core.FakeServices
{

    public class FakeDevicesService : FakeEntitiesService<Device>, IDevicesService
    {
        public FakeDevicesService(DeviceFaker faker) : base(faker)
        {
        }

        public Device Get(string name) => entites.FirstOrDefault(d => d.Name == name);
        
    }

    //public class FakeDevicesService : IDevicesService
    //{
    //    private readonly ICollection<Device> devices;

    //    public FakeDevicesService(DeviceFaker deviceFaker)
    //    {
    //        devices = deviceFaker.Generate(100);
    //    }


    //    public void Add(Device device) => devices.Add(device);

    //    public IEnumerable<Device> Get() => devices;

    //    public Device Get(int id) => devices.SingleOrDefault(d => d.Id == id);

    //    public Device Get(string name)
    //    {
    //        return devices.FirstOrDefault(d => d.Name == name);
    //    }

    //    public void Remove(int id)
    //    {
    //        Device device = Get(id);

    //        devices.Remove(device);
    //    }

    //    public void Update(Device device)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
