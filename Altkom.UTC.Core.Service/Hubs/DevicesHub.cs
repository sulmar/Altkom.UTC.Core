using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Hubs
{
    public interface IDevicesHubClient
    {
        Task Added(Device device);
    }


    // Strong typed hub
    public class DevicesHub : Hub<IDevicesHubClient>
    {

        public void Send(Device device)
        {
            this.Clients.All.Added(device);
        }
    }
}
