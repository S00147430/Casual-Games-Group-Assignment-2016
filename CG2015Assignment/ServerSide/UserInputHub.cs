using Microsoft.AspNet.SignalR;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide
{
    public class UserInputHub : Hub
    {
        public string v1, v2;
        UsersDb db = new UsersDb();

        //Receive values

        private void recieved_a_message(string name, string password)
        {
            v1 = name;
            v2 = password;
        }

        public void Send(string name, string password)
        {
            v1 = name; v2 = password;
        }


        public UserInputHub() : base()
        {

        }

        public UsersDb context = new List<UsersCreate>
        {
            new UsersCreate { PlayerName = , Password = "Xavier"},
        };

        //initPlayers.ForEach(u => context.UsersDatabase.Add(u));
        //context.SaveChanges();
        }
    }
}