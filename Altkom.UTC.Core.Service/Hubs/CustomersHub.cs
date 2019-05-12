using Altkom.UTC.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UTC.Core.Service.Hubs
{

    [Authorize]
    public class CustomersHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await this.Groups.AddToGroupAsync(Context.ConnectionId, groupName: "Developers");

            await base.OnConnectedAsync();
        }

        public Task CustomerAdded(Customer customer)
        {
            return this.Clients.Others.SendAsync("Added", customer);
        }
    }
}
