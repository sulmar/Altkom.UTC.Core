using Altkom.UTC.Core.Models;

namespace Altkom.UTC.Core.IServices
{
    public interface IDevicesService : IEntiesService<Device>
    { 
        Device Get(string name);
    }



}
