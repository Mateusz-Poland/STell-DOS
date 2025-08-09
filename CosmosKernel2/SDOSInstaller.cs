using System;
using System.ComponentModel;
using System.IO;
using Sys = Cosmos.System;

namespace CosmosKernel2
{
    public static class SDOSInstall
    {
        public static void DOSFirstStart()
        {
            Console.WriteLine("[S-DOS] Start Installing OS...");
            InstallProcess();
        }

        private static void InstallProcess()
        {
            Kernel.ClearConsole();
            Directory.CreateDirectory(@"0:\SDOS");

            System.Console.WriteLine("[S-DOS] S-DOS INSTALLER: Welcome to the "+Kernel.sdosVersion + " installer!");

            System.Console.Write("[S-DOS] S-DOS INSTALLER: Enter your username: ");
            string username = System.Console.ReadLine();
            System.Console.Write("[S-DOS] S-DOS INSTALLER: Enter your password: ");
            string cPassword = System.Console.ReadLine();

            System.Console.WriteLine("[S-DOS] S-DOS INSTALLER: Creating System Directory...");
            System.Console.WriteLine("[S-DOS] S-DOS INSTALLER: Creating File for user...");
            Kernel.dosFS.CreateFile("0:\\SDOS\\users.db");
            Kernel.dosFS.CreateFile("0:\\SDOS\\password.db");
            System.Console.WriteLine("[S-DOS] S-DOS INSTALLER: Setting User Preferences...");
            File.WriteAllText("0:\\SDOS\\users.db", username);     //This will save username
            File.WriteAllText("0:\\SDOS\\password.db", cPassword); //this one will save the user password

            File.Delete(@"0:\test\DirInTest\Readme.txt");
            Directory.Delete(@"0:\test\DirInTest");
            Directory.Delete(@"0:\test");
            Directory.Delete(@"0:\Dir Testing");
            File.Delete(@"0:\Kudzu.txt");
            File.Delete(@"0:\Root.txt");

            Directory.CreateDirectory(@"0:\"+username);

            Console.WriteLine("[S-DOS] Stel-DOS has been installed.");

            System.Threading.Thread.Sleep(500);

            Kernel.ClearConsole();

            Sys.PCSpeaker.Beep(680, 150);
            System.Threading.Thread.Sleep(100);
            Sys.PCSpeaker.Beep(498, 150);
            System.Threading.Thread.Sleep(100);
            Sys.PCSpeaker.Beep(743, 150);
            System.Threading.Thread.Sleep(100);
            Sys.PCSpeaker.Beep(580, 700);
            System.Threading.Thread.Sleep(70);
        }
    }
}
