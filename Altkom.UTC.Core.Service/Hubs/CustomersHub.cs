using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Hubs
{
    public class CustomersHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task CustomerAdded(Customer customer)
        {
            return this.Clients.Others.SendAsync("Added", customer);
        }
    }
}
