using Microsoft.AspNet.SignalR.Client;
using ServerSide;
using ServerSide.Models;
using ServerSide.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class LoginProgram
    {
        static IHubProxy proxy;
        static bool isValidated = false;
        static string name, password;

        static void Main(string[] args)
        {
            HubConnection connection = new HubConnection("http://localhost:56859");
            proxy = connection.CreateHubProxy("UserInputHub");

            //connection.Received += Connection_Received;

            Action<int> sendPlayerNum = recieved_player_num;

            Action<string, string> SendMessagerecieved = recieved_a_message;
            Action<bool> sendValidated = recieved_validated;
            proxy.On("BroadcastMessage", SendMessagerecieved);
            connection.Start().Wait();


            ModelUsers1 db = new ModelUsers1();
            UsersViewModel uvm = new UsersViewModel();

            //UsersDb db = new UsersDb();
            name = null;
            password = null;

            Console.Write("User Name:");
            name = Console.ReadLine();

            Console.Write("\nPassword:");
            password = Console.ReadLine();

            if (name == null || password == null)
            {
                Console.Write("Please fill in all the details.");
            }

            if (name != null || password != null)
            {
                proxy.Invoke("RequestValidation", new object[] { name, password });
                proxy.Invoke("RequestValidated");
            }

            //ServerSide.Models.UsersDb
        }

        private static void recieved_a_message(string Name, string Message)
        {
            Console.WriteLine("{0} : {1}", Name, Message);
        }

        private static void recieved_validated(bool recievedValidated)
        {
            if (recievedValidated)
            {
                isValidated = true;
                
            }

        }

        private static void recieved_player_num(int playerNum)
        {
            //recieves index of player, to use as reference.Still have problem of not being able to reference this from the ClientGame
        }

        public bool GetValidated()
        {
            return isValidated;
        }

        public string GetName()
        {
            return name;
        }
    }
}
