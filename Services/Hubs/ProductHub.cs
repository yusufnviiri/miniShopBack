using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace Services.Hubs
{
    




        public class ProductHub : Hub
        {
            // Called when client connects
            public override async Task OnConnectedAsync()
            {
                await base.OnConnectedAsync();
            }

            // Client subscribes to a product
            public async Task JoinProductGroup(string productId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, productId);
            }
        }
    }



