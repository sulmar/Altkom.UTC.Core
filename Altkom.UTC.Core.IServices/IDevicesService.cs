using Altkom.UTC.Core.Models;
using System.Collections.Generic;

namespace Altkom.UTC.Core.IServices
{
    public interface IDevicesService
    {
        IEnumerable<Device> Get();
        Device Get(int id);
        void Add(Device device);
        void Update(Device device);
        void Remove(int id);

        Device Get(string name);
    }
}
