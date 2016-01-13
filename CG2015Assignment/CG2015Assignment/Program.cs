using Microsoft.AspNet.SignalR.Client;
using System;

namespace CG2015Assignment
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        static string name;
        static IHubProxy proxy;
        static int V1 = 2, V2 = 5;

        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Game1())
                game.Run();

            Console.WriteLine("Enter your Name:");
            name = Console.ReadLine();

            HubConnection connection = new HubConnection("http://localhost:13639");
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
            Console.WriteLine("The state has been changed.");
        }

        private static void recieved_a_message(string Name, string Message)
        {
            Console.WriteLine("{0} : {1} : {3} : {4}", Name, Message);
        }

        private static void Connection_Received(string obj)
        {
            Console.WriteLine("Message Recieved {0}", obj);
        }
    }
#endif
}
