using Microsoft.AspNet.SignalR;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerSide
{
    public class UserInputHub : Hub
    {
        bool isValid;
        public static string v1 = "", v2 = "";
        ModelUsers1 db = new ModelUsers1();

        public static List<string> currentPlayers;

        //Receive values

        //private void recieved_a_message(string name, string password)
        //{
        //    v1 = name;
        //    v2 = password;
        //}

        public void SendRegDetails(string name, string password)
        {
            v1 = name; v2 = password;
            db.UsersCreates.Add(new UsersCreate { PlayerName = v1, Password = v2 });
            db.SaveChanges();
        }

        public void RequestValidation(string name, string password)
        {
            isValid = false;
            foreach(var user in db.UsersCreates)
            {
                if (user.PlayerName == name && user.Password == password)
                {
                    isValid = true;
                    //currentPlayers.Add(name);
                    //Clients.Caller.sendPlayerNum(currentPlayers.IndexOf(name));
                }

            }
        }

        public void RequestScoreboard(string name)
        {
            foreach (var user in db.UsersCreates)
            {
                if (user.PlayerName == name)
                {
                    foreach (var score in db.ScoreBoards)
                    {
                        if (user.ScoreBoard_id == score.id)
                            Clients.Caller.sendScoreboard(score.Score);
                    }
                }
            }
        }

        public void RequestValidated()
        {
            Clients.Caller.sendValidated(isValid);
        }


        public UserInputHub() : base()
        {

        }

        //public UsersDb context = new List<UsersCreate>
        //{
        //    new UsersCreate { PlayerName = , Password = "Xavier"},
        //};

        //public class RegisterIntialize : DropCreateDatabaseAlways<ModelUsers1>
        //{
        //    //Take in Values

        //    protected override void Seed(ModelUsers1 context)
        //    {
        //        var initPlayers = new List<UsersCreate>
        //        {
        //            new UsersCreate { PlayerName = v1, Password = v2}
        //        };

        //        initPlayers.ForEach(u => context.UsersCreates.Add(u));
        //        context.SaveChanges();
        //    }
        //}
    }  
}