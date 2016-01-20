using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class ChatProgram
    {
        static string name;
        //can't reference name due to a myriad of problems, will regrettably not be used.
        static IHubProxy proxy;
        static int V1 = 2, V2 = 5;

        [STAThread]
        static void Main(string[] args)
        {
            HubConnection connection = new HubConnection("http://localhost:56859");
            proxy = connection.CreateHubProxy("ChatHub");

            connection.Received += Connection_Received;

            Action<string> SendMessagerecieved = recieved_a_message;
            proxy.On("BroadcastMessage", SendMessagerecieved);

            connection.Start().Wait();

            connection.StateChanged += Connection_StateChanged;

            proxy.Invoke("CountUsers", new object[] {  });

            string input;
            while ((input = Console.ReadLine()) != null)
            {
                proxy.Invoke("Send", new object[] { input });
            }
        }

        private static void Connection_StateChanged(StateChange sc)
        {
             
        }

        private static void recieved_a_message(string Message)
        {
            Console.WriteLine("{0}", Message);
        }

        private static void Connection_Received(string obj)
        {
            Console.WriteLine("{0}", obj);
        }
    }
}
