using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide.ViewModel
{
    public class UsersViewModel
    {
        public int UsersCount { get; set; }
        public int AchievementsCount { get; set; }
        public List<UserRegister> UsersList { get; set; }
    }
}