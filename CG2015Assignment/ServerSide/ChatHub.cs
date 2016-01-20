using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide
{
    public class ChatHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.broadcastMessage(message);
        }

        public void CountUsers(int no)
        {
            Clients.All.broadcastMessage(no);
        }

        public ChatHub() : base()
        {

        }
    }
}