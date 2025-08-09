using CosmosKernel2;
using System;
using System.IO;
using Sys = Cosmos.System;

namespace CosmosKernel2
{
    internal class BootManager
    {
        public static void SDOSLoaded()
        {
            Sys.PCSpeaker.Beep(666, 500);
            if (!Directory.Exists(@"0:\SDOS"))
            {
                SDOSInstall.DOSFirstStart();
            }
            else
            {
                Kernel.ClearConsole();
                string password = File.ReadAllText("0:\\SDOS\\password.db");
                string username = File.ReadAllText("0:\\SDOS\\users.db");
                System.Console.WriteLine("[S-DOS] LOADING...");
                bool loggedIN = false;
                while (!loggedIN)
                {
                    System.Console.Write("[S-DOS] Enter the user password: ");
                    string type = System.Console.ReadLine();

                    if (type == password)
                    {
                        System.Console.WriteLine("[S-DOS] Loading S-DOS...");
                        System.Threading.Thread.Sleep(1000);

                        Kernel.ClearConsole();

                        System.Console.WriteLine("[S-DOS] "+Kernel.sdosVersion + " is ready for use.");
                        loggedIN = true;

                        Sys.PCSpeaker.Beep(680, 150);
                        System.Threading.Thread.Sleep(100);
                        Sys.PCSpeaker.Beep(498, 150);
                        System.Threading.Thread.Sleep(100);
                        Sys.PCSpeaker.Beep(743, 150);
                        System.Threading.Thread.Sleep(100);
                        Sys.PCSpeaker.Beep(580, 700);
                        System.Threading.Thread.Sleep(70);

                        System.Console.WriteLine("[S-DOS] Welcome, " + username + "!");

                    }
                    else
                    {
                        System.Console.WriteLine("[S-DOS] Incorrect password. Try again!");
                    }
                }
            }
        }
    }
}
