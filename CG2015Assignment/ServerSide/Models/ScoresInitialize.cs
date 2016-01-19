using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerSide.Models
{
    public class ScoresInitialize : DropCreateDatabaseAlways<UsersDb>
    {
        protected override void Seed(UsersDb context)
        {
            var initPlayers = new List<UserRegister>
            {

            };

        initPlayers.ForEach(u => context.UsersDatabase.Add(u));
        context.SaveChanges();
        }
    }
}