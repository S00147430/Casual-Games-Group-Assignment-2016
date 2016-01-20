using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register
{
    //Send Values
    public class RegisterProgram
    {
        static IHubProxy proxy;
        static string V1, V2;

        public RegisterProgram()
        {
        }

        static void Main(string[] args)
        {
            string name = null, password = null, confirm = null;
            int p = 1, c = 2;

            HubConnection connection = new HubConnection("http://localhost:56859");
            proxy = connection.CreateHubProxy("UserInputHub");

            //connection.Received += Connection_Received;

            Action<string, string> SendMessagerecieved = recieved_a_message;
            proxy.On("BroadcastMessage", SendMessagerecieved);
            connection.Start().Wait();

            while (p != c)
            {
                Console.Write("\nEnter your User Name:\n");
                name = Console.ReadLine();

                Console.Write("Enter your Password:\n");
                password = Console.ReadLine();

                Console.Write("Confirm your Password:\n");
                confirm = Console.ReadLine();

                if (confirm == password)
                {
                    c = 1;
                }

                else
                {
                    Console.Write("\nPasswords don't match.");
                }

                if (name == null || password == null || confirm == null)
                {
                    Console.Write("Please fill in all the deatils.");
                }

                if (name != null || password != null)
                {
                    proxy.Invoke("SendRegDetails", new object[] { name, password });
                }
            }
        }

        private static void recieved_a_message(string Name, string Message)
        {
            Console.WriteLine("{0} : {1}", Name, Message);
        }
    }
}

