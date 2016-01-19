using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class Program
    {
        static string name;
        static IHubProxy proxy;
        static int V1 = 2, V2 = 5;

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Name:");
            name = Console.ReadLine();

            HubConnection connection = new HubConnection("http://localhost:56858");
            proxy = connection.CreateHubProxy("ChatHub");

            connection.Received += Connection_Received;

            Action<string, string> SendMessagerecieved = recieved_a_message;
            proxy.On("BroadcastMessage", SendMessagerecieved);

            connection.Start().Wait();

            connection.StateChanged += Connection_StateChanged;

            string input;
            while ((input = Console.ReadLine()) != null)
            {
                proxy.Invoke("Send", new object[] { name, input });
            }
        }

        private static void Connection_StateChanged(StateChange sc)
        {
             
        }

        private static void recieved_a_message(string Name, string Message)
        {
            Console.WriteLine("{0} : {1}", Name, Message);
        }

        private static void Connection_Received(string obj)
        {
            Console.WriteLine("{0}", obj);
        }
    }
}
