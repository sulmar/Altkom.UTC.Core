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

        public Task Send(Device device)
        {
            return this.Clients.All.Added(device);
        }
    }
}
