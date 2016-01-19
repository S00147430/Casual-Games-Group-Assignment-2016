using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerSide.Models
{
    public class RegisterIntialize : DropCreateDatabaseAlways<UsersDb>
    {
        //Take in Values
        //string name = UserInputHub.Equals., password;

        protected override void Seed(UsersDb context)
        {
            var initPlayers = new List<UsersCreate>
            {
                //new UsersCreate { PlayerName = , Password = password},
            };

            initPlayers.ForEach(u => context.UsersDatabase.Add(u));
            context.SaveChanges();
        }
    }
}