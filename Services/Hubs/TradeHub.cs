using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Hubs
{
        public class TradeHub : Hub
        {
            // Called when client connects
            public override async Task OnConnectedAsync()
            {
                await base.OnConnectedAsync();
            }

            // Client subscribes to a product
            public async Task JoinTradeGroup(string tradeId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, tradeId);
            }
        }

    }

