using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerSide.Models
{
    public class UserInitialize : DropCreateDatabaseAlways<UsersDb>
    {
        protected override void Seed(UsersDb context)
        {
            var initPlayers = new List<UsersCreate>
            {
              new UsersCreate { PlayerName = "XMan", Password = "Xavier", AchievementsList = new List<achievements> {
                new achievements() { Title = "Fox"},
                new achievements() { Title = "Duet"},
                new achievements() { Title = "You're Winner"},
                new achievements() { Title = "First to die"},
                new achievements() { Title = "Can't be beat"}
                } },
            };

            initPlayers.ForEach(u => context.UsersDatabase.Add(u));
            context.SaveChanges();
        }
    }
}