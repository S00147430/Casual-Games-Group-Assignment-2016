using ServerSide.Models;
using ServerSide.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide.Controllers
{
    public class UserController : Controller
    {
        UsersDb db = new UsersDb();
        UsersViewModel uvm = new UsersViewModel();

        public ActionResult Index()
        {
            uvm.UsersList = db.UsersDatabase.Include("UsersList").ToList();
            uvm.UsersCount = uvm.UsersList.Count();
            //uvm.UsersList.ForEach(users => uvm.UsersCount += movie.ActorsList.Count());
            ViewBag.Title = "CA2";

            return View(uvm);
        }
    }
}