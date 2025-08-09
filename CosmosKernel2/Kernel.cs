/*
 * Stell Disk Operating System 1.1
 * by Mateusz |aka| MrHan |aka| cherry
 * Stell (c) 2021-2030
 */

using Cosmos.Core;
using Cosmos.Core.IOGroup;
using Cosmos.HAL.BlockDevice;
using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics.Fonts;
using Cosmos.System.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;

namespace CosmosKernel2
{
    public class Kernel : Sys.Kernel
    {
        public static string fileName;

        public static string Version = "Version 1.3";
        public static Sys.FileSystem.CosmosVFS dosFS;

        public static string CurrDir = @"0:\";
        public static string sdosVersion = "S-DOS Version 1.3";


        protected override void BeforeRun()
        {
            System.Console.WriteLine("[S-DOS] Start STell-DOS...");
            dosFS = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(dosFS);
            BootManager.SDOSLoaded();
        }

        protected override void Run()
        {
            System.Console.Write(CurrDir);
            PROGRAMMS.Interpreter();
        }

        public static void nano(string input)
        {
            string path = input;
            System.Console.WriteLine("[S-DOS] make in:  " + CurrDir + path);
            try
            {
                Nano fm = new Nano();
                if (File.Exists(CurrDir + path))
                {
                    fm.initNano(CurrDir + path);
                }
                else if (File.Exists(CurrDir + path))
                {
                    fm.initNano(path);
                }
                else
                {
                    dosFS.CreateFile(CurrDir + path);
                    fm.initNano(CurrDir + path);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public static void ClearConsole()
        {
            System.Console.Clear();
            System.Console.BackgroundColor = ConsoleColor.Blue;
            System.Console.WriteLine(" Stell-DOS 1.3                                               " + DateTime.Now);
            System.Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
