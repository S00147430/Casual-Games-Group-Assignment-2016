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
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
