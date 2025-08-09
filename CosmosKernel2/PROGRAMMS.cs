using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel2
{
    internal class PROGRAMMS
    {
        public static void Interpreter()
        {
            var input = System.Console.ReadLine();
            string FileName = "";
            string DirName = "";

            switch (input)
            {
                default:

                    System.Console.WriteLine("[S-DOS] UNKNOWN COMMAND.");
                    Cosmos.System.PCSpeaker.Beep(500, 400);
                    break;

                case "help":
                    System.Console.WriteLine("");
                    System.Console.Write("[S-DOS] Enter the page number: ");
                    string pager = System.Console.ReadLine();

                    if (int.TryParse(pager, out int pagerNum))
                    {
                        if (pagerNum < 1)
                        {
                            System.Console.WriteLine("[S-DOS] You entered the wrong page.");
                            break;
                        }
                        if (pagerNum > 2)
                        {
                            System.Console.WriteLine("[S-DOS] You entered the wrong page.");
                            break;
                        }
                        if (pagerNum == 1)
                        {

                            System.Console.WriteLine("");
                            System.Console.WriteLine("");
                            System.Console.WriteLine("[S-DOS] COMMANDS:");
                            System.Console.WriteLine("  help            : List of available commands. ");
                            System.Console.WriteLine("  shutdown        : Turn off the device. ");
                            System.Console.WriteLine("  reboot          : Reboot the device. ");
                            System.Console.WriteLine("  deviceinfo      : Information about the device. ");
                            System.Console.WriteLine("  datetime        : Current time. ");
                            System.Console.WriteLine("  update          : Clear the console. ");
                            System.Console.WriteLine("  systeminfo      : Information about the system. ");
                            System.Console.WriteLine("  echo            : Outputs text. ");
                            System.Console.WriteLine("  dir             : List of files in the directory. ");
                            System.Console.WriteLine("  makefile        : Creates a new file. ");
                            System.Console.WriteLine("  makedir         : Creates a new dir. ");
                            System.Console.WriteLine("  curcd           : The directory you are in. ");
                            System.Console.WriteLine("  cd              : Go to the directory. ");
                            System.Console.WriteLine("  deletefile      : Deletes the file. ");
                            System.Console.WriteLine("  deletedir       : Deletes the dir. ");
                            System.Console.WriteLine("  version         : Version of OS. ");
                            System.Console.WriteLine("  math            : Solving mathematical problems. ");
                            System.Console.WriteLine("  notebook        : NoteBook Text Editor. ");
                            System.Console.WriteLine("  openfile        : Displays the contents of the text file. ");
                            System.Console.WriteLine("  cdspace         : Free space in the current directory. ");
                            System.Console.WriteLine("");
                            System.Console.WriteLine("");
                        }
                        if (pagerNum == 2)
                        {

                            System.Console.WriteLine("");
                            System.Console.WriteLine("");
                            System.Console.WriteLine("[S-DOS] COMMANDS:");
                            System.Console.WriteLine("  renamefile      : Rename the text file. ");
                            System.Console.WriteLine("  back            : Go to upper directory. ");
                            System.Console.WriteLine("  formatdisk      : Disk formatting. ");
                            System.Console.WriteLine("  editfile        : Editing the file. ");
                            System.Console.WriteLine("  copyfile        : Copying the file. ");
                            System.Console.WriteLine("");
                            System.Console.WriteLine("");
                        }
                    }
                    else
                        System.Console.WriteLine("[S-DOS] You entered the wrong page.");
                    break;

                case "back":
                    if (Kernel.CurrDir != @"0:\")
                    {
                        string[] pieces = Kernel.CurrDir.Split(@"\");
                        string newPathex = "";
                        for (int x = 0; x != pieces.Length - 2; x++)
                        {
                            newPathex += pieces[x] + @"\";
                        }
                        Kernel.CurrDir = newPathex;
                        break;
                    }
                    else
                    {
                        System.Console.WriteLine("[S-DOS] ERROR: Can't go upper than root folder!");
                        Kernel.CurrDir = @"0:\";
                    }
                    break;

                case "cdspace":
                    var available_space = Kernel.dosFS.GetAvailableFreeSpace(@"0:\");
                    var aviab_spac = available_space / 1024;
                    var aviab_space = aviab_spac / 1024;
                    if (aviab_spac < 0)
                    {
                        System.Console.WriteLine("Available Free Space: " + available_space + "B");
                    }
                    else if (aviab_space < 0)
                    {
                        System.Console.WriteLine("Available Free Space: " + aviab_spac + "KB");
                    }
                    else
                        System.Console.WriteLine("Available Free Space: " + aviab_space + "MB");
                    System.Console.WriteLine("");
                    break;

                case "renamefile":
                    System.Console.WriteLine("");
                    System.Console.WriteLine("[S-DOS] FILE MANAGER: Enter the name of the file to rename.");
                    System.Console.WriteLine("[S-DOS] FILE MANAGER: You are in the directory: " + Kernel.CurrDir);
                    System.Console.Write("[S-DOS] ENTER FULL PATH TO FILE: ");
                    string oldPath = System.Console.ReadLine();
                    System.Console.Write("[S-DOS] ENTER NEW FILE NAME WITH FULL PATH: ");
                    string newPath = System.Console.ReadLine();

                    if (!File.Exists(oldPath))
                    {
                        System.Console.WriteLine("[S-DOS] FILE MANAGER: File does not exist: " + oldPath + " .");
                    }
                    else
                    {
                        try
                        {
                            System.Console.WriteLine("[S-DOS] The file " + oldPath + " has been renamed to " + newPath + " .");
                            string contenta = File.ReadAllText(oldPath);
                            File.WriteAllText(newPath, contenta);
                            File.Delete(oldPath);

                            System.Console.WriteLine("[S-DOS] The file " + oldPath + " has been renamed to " + newPath + " .");
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
                        }
                    }
                    break;

                case "formatdisk":
                    System.Console.Write("[S-DOS] FILE MANAGER: This will erase all data. Continue? (y/n): ");
                    if (System.Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        System.Console.WriteLine("");
                        try
                        {
                            foreach (string dir in Directory.GetDirectories(@"0:\"))
                            {
                                if (!dir.Equals(@"0:\SDOS", StringComparison.OrdinalIgnoreCase))
                                    Directory.Delete(dir, true);
                            }
                            foreach (string file in Directory.GetFiles(@"0:\"))
                            {
                                File.Delete(file);
                            }
                            System.Console.WriteLine("[S-DOS] Disk formatting was successfully completed.");
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine("[S-DOS] ERROR: " + ex.Message);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("\n[S-DOS] Format cancelled.");
                    }
                    break;

                case "editfile":
                    System.Console.Write("[S-DOS] Enter file name to edit: ");
                    string editFile = System.Console.ReadLine();
                    if (!File.Exists(editFile) && !File.Exists(Kernel.CurrDir + editFile))
                    {
                        System.Console.WriteLine("[S-DOS] File not found.");
                        break;
                    }

                    if (!File.Exists(editFile))
                        editFile = Kernel.CurrDir + editFile;

                    string oldText = File.ReadAllText(editFile);
                    System.Console.WriteLine("[S-DOS] Current file content:");
                    System.Console.WriteLine("--------------------------------");
                    System.Console.WriteLine(oldText);
                    System.Console.WriteLine("--------------------------------");
                    System.Console.WriteLine("[S-DOS] Enter new content (end with empty line):");

                    string newText = "";
                    string line;
                    while (!string.IsNullOrEmpty(line = System.Console.ReadLine()))
                    {
                        newText += line + "\n";
                    }

                    File.WriteAllText(editFile, newText);
                    System.Console.WriteLine("[S-DOS] File saved.");
                    break;

                case "copyfile":
                    System.Console.Write("[S-DOS] Enter source file path: ");
                    string sourcePath = System.Console.ReadLine();
                    System.Console.Write("[S-DOS] Enter destination file path: ");
                    string destPath = System.Console.ReadLine();
                    try
                    {
                        if (!File.Exists(sourcePath) && !File.Exists(Kernel.CurrDir + sourcePath))
                        {
                            System.Console.WriteLine("[S-DOS] Source file not found.");
                            break;
                        }

                        if (!File.Exists(sourcePath))
                            sourcePath = Kernel.CurrDir + sourcePath;

                        File.Copy(sourcePath, destPath, true);
                        System.Console.WriteLine("[S-DOS] File copied successfully.");
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("[S-DOS] ERROR: " + ex.Message);
                    }
                    break;

                case "dir":
                    int totalLen = 25;
                    bool isWhite = true;

                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.Write("--------------------------------------------------------------------------------");
                    foreach (string dir in Directory.GetDirectories(Kernel.CurrDir))
                    {
                        if (isWhite == true)
                        {
                            System.Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            System.Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        isWhite = !isWhite;

                        System.Console.Write(dir);
                        for (int x = 0; x != totalLen - dir.Length; x++)
                        {
                            System.Console.Write(" ");
                        }
                        System.Console.Write("<dir>");

                        if (Directory.GetDirectories(Kernel.CurrDir + dir).Length < 1 && Directory.GetFiles(Kernel.CurrDir + dir).Length < 1)
                        {
                            System.Console.WriteLine("   (empty)");
                        }
                        else
                        {
                            System.Console.Write("\n");
                        }
                    }
                    foreach (string file in Directory.GetFiles(Kernel.CurrDir))
                    {
                        if (isWhite == true)
                        {
                            System.Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            System.Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        isWhite = !isWhite;

                        System.Console.Write(file);
                        int size = (int)new FileInfo(Kernel.CurrDir + file).Length;
                        for (int x = 0; x != totalLen - file.Length; x++)
                        {
                            System.Console.Write(" ");
                        }
                        System.Console.Write(size.ToString() + "B\n");
                    }
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.Write("--------------------------------------------------------------------------------");
                    System.Console.ForegroundColor = ConsoleColor.White;
                    break;

                case "openfile":
                    System.Console.WriteLine("");
                    System.Console.Write("[S-DOS] FILE MANAGER: OPEN FILE: ");

                    string pathero = System.Console.ReadLine();

                    if (!pathero.EndsWith(".text", StringComparison.OrdinalIgnoreCase))
                    {
                        pathero += ".text";
                    }

                    try
                    {
                        if (File.Exists(Kernel.CurrDir + pathero))
                        {
                            System.Console.WriteLine("[S-DOS] The contents of the file: ");
                            System.Console.WriteLine(File.ReadAllText(Kernel.CurrDir + pathero));
                            System.Console.WriteLine("");
                            System.Console.WriteLine("");
                        }
                        else if (File.Exists(pathero))
                        {
                            System.Console.WriteLine("[S-DOS] The contents of the file: ");
                            System.Console.WriteLine(File.ReadAllText(pathero));
                            System.Console.WriteLine("");
                            System.Console.WriteLine("");
                        }
                        else
                            System.Console.WriteLine("[S-DOS] FILE MANAGER: File does not exist: " + Kernel.CurrDir + pathero);
                        System.Console.WriteLine("");
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                    break;

                case "makefile":
                    System.Console.Write("[S-DOS] FILE MANAGER: Enter the name of the new file: ");
                    FileName = System.Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(FileName))
                    {
                        System.Console.WriteLine("[S-DOS] FILE MANAGER: Invalid file name.");
                        break;
                    }

                    try
                    {
                        string fullPath = Kernel.CurrDir + FileName;

                        string lowerPatheq = fullPath.ToLower();
                        if (lowerPatheq == @"0:\sdos" || lowerPatheq.StartsWith(@"0:\sdos\"))
                        {
                            System.Console.WriteLine("[S-DOS] FILE MANAGER: ERROR. Access denied.");
                            break;
                        }

                        if (Directory.Exists(fullPath))
                        {
                            System.Console.WriteLine("[S-DOS] A directory with that name already exists.");
                            break;
                        }

                        if (File.Exists(fullPath))
                        {
                            System.Console.WriteLine("[S-DOS] File already exists.");
                            break;
                        }
                        File.Create(fullPath);
                        System.Console.WriteLine("[S-DOS] FILE MANAGER: File \"" + fullPath + "\" created.");
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("[S-DOS] ERROR: " + e.Message);
                    }
                    break;

                case "makedir":
                    System.Console.Write("[S-DOS] FILE MANAGER: Enter the name of the new DIR: ");
                    DirName = System.Console.ReadLine();
                    string lowerPathener = DirName.ToLower();
                    try
                    {

                        if (lowerPathener == "sdos" || lowerPathener == "0:\\sdos")
                        {
                            System.Console.WriteLine("[S-DOS] FILE MANAGER: ERROR.");
                            break;
                        }

                        else if (!Directory.Exists(Kernel.CurrDir + DirName))
                            Kernel.dosFS.CreateDirectory(Kernel.CurrDir + DirName);

                        else if (!Directory.Exists(DirName))
                            Kernel.dosFS.CreateDirectory(DirName);
                        else
                            System.Console.WriteLine("[S-DOS] FILE MANAGER: File/Directory already exists: " + Kernel.CurrDir + DirName);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                    break;

                case "notebook":
                    System.Console.WriteLine("[S-DOS] You are in the directory " + Kernel.CurrDir + ".");
                    System.Console.Write("[S-DOS] Enter a name for the new text file: ");
                    string NameOfFile = System.Console.ReadLine();
                    if (!NameOfFile.EndsWith(".text", StringComparison.OrdinalIgnoreCase))
                    {
                        NameOfFile += ".text";
                    }
                    Kernel.nano(NameOfFile);
                    break;

                case "curcd":
                    System.Console.WriteLine("[S-DOS] You are in the directory " + Kernel.CurrDir + ".");
                    break;

                case "cd":
                    System.Console.Write(Kernel.CurrDir + " FILE MANAGER: Enter DIR name: ");
                    string path = System.Console.ReadLine();

                    string pathet = Kernel.CurrDir + path + @"\";
                    string lowerPath = path.ToLower();
                    try
                    {

                        if (lowerPath == "sdos" || lowerPath == "0:\\sdos")
                        {
                            System.Console.WriteLine("[S-DOS] FILE MANAGER: This directory is not accessible.");
                            break;
                        }

                        if (Directory.Exists(pathet))
                        {
                            System.Console.WriteLine("");
                            Kernel.CurrDir = pathet;
                            System.Console.WriteLine("");

                            int totalLener = 25;
                            bool isWhiter = true;

                            System.Console.ForegroundColor = ConsoleColor.Cyan;
                            System.Console.Write("--------------------------------------------------------------------------------");
                            foreach (string dir in Directory.GetDirectories(Kernel.CurrDir))
                            {
                                if (isWhiter == true)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                isWhiter = !isWhiter;

                                System.Console.Write(dir);
                                for (int x = 0; x != totalLener - dir.Length; x++)
                                {
                                    System.Console.Write(" ");
                                }
                                System.Console.Write("<dir>");

                                if (Directory.GetDirectories(Kernel.CurrDir + dir).Length < 1 && Directory.GetFiles(Kernel.CurrDir + dir).Length < 1)
                                {
                                    System.Console.WriteLine("   (empty)");
                                }
                                else
                                {
                                    System.Console.Write("\n");
                                }
                            }
                            foreach (string file in Directory.GetFiles(Kernel.CurrDir))
                            {
                                if (isWhiter == true)
                                {
                                    System.Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                {
                                    System.Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                isWhiter = !isWhiter;

                                System.Console.Write(file);
                                int size = (int)new FileInfo(Kernel.CurrDir + file).Length;
                                for (int x = 0; x != totalLener - file.Length; x++)
                                {
                                    System.Console.Write(" ");
                                }
                                System.Console.Write(size.ToString() + "B\n");
                            }
                            System.Console.ForegroundColor = ConsoleColor.Cyan;
                            System.Console.Write("--------------------------------------------------------------------------------");
                            System.Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            System.Console.WriteLine("File does not exist " + Kernel.CurrDir + path);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                    break;

                case "deletefile":
                    System.Console.Write("[S-DOS] Enter the name file: ");
                    FileName = System.Console.ReadLine();
                    try
                    {
                        if (!File.Exists(Kernel.CurrDir + FileName))
                            System.Console.WriteLine("[S-DOS] The file does not exist..");
                        else
                        {
                            if (FileName == "SDOS")
                                System.Console.WriteLine("[S-DOS] ERROR: YOU CANNOT DELETE A SYSTEM FILE.");
                            else
                                File.Delete(Kernel.CurrDir + FileName);
                            System.Console.WriteLine("[S-DOS] File '" + Kernel.CurrDir + FileName + "' has been deleted.");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                    }
                    break;

                case "deletedir":
                    System.Console.Write("[S-DOS] Enter the name DIR: ");
                    DirName = System.Console.ReadLine();
                    try
                    {
                        if (!Directory.Exists(Kernel.CurrDir + DirName))
                            System.Console.WriteLine("[S-DOS] The DIR does not exist..");
                        else
                        {
                            if (FileName == "SDOS")
                            {
                                System.Console.WriteLine("[S-DOS] ERROR: YOU CANNOT DELETE A SYSTEM FILE.");
                            }

                            System.Console.Write("[S-DOS] Do you really want to turn off the device? (y/n) ");
                            if (System.Console.ReadKey().Key == ConsoleKey.Y)
                            {
                                System.Console.WriteLine("");
                                Directory.Delete(Kernel.CurrDir + DirName);
                                System.Console.WriteLine("[S-DOS] DIR '" + Kernel.CurrDir + DirName + "' has been deleted.");
                            }
                            else
                            {
                                System.Console.WriteLine("");
                                System.Console.WriteLine("[S-DOS] FILE MANAGER: Cancel...");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                    }
                    break;

                case "datetime":
                    System.Console.WriteLine("[S-DOS] " + Time.Hour() + ":" + Time.Minute() + ":" + Time.Second());
                    System.Console.WriteLine("[S-DOS] Date is (M/D/Y): " + Time.Month() + "/" + Time.DayOfMonth() + "/" + Time.Century() + Time.Year() + " Day: " + Time.DayOfWeek());
                    break;

                case "update":
                    System.Console.WriteLine("[S-DOS] Update...");
                    System.Threading.Thread.Sleep(1000);
                    Kernel.ClearConsole();

                    break;

                case "shutdown":
                    System.Console.Write("[S-DOS] Do you really want to turn off the device? (y/n) ");
                    if (System.Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        System.Console.WriteLine("");
                        System.Console.WriteLine("[S-DOS] Shutdown...");

                        System.Threading.Thread.Sleep(100);
                        Cosmos.System.PCSpeaker.Beep(475, 80);
                        System.Threading.Thread.Sleep(100);
                        Cosmos.System.PCSpeaker.Beep(501, 300);
                        System.Threading.Thread.Sleep(100);
                        Cosmos.System.PCSpeaker.Beep(600, 600);
                        System.Threading.Thread.Sleep(100);
                        Power.ShutDown();
                    }
                    else
                        System.Console.WriteLine("");
                    System.Console.WriteLine("[S-DOS] Cancel.");
                    break;

                case "reboot":
                    System.Console.WriteLine("[S-DOS] Rebooting in 3 seconds...");
                    System.Threading.Thread.Sleep(1000);
                    System.Console.WriteLine("[S-DOS] Rebooting in 2 seconds...");
                    System.Threading.Thread.Sleep(1000);
                    System.Console.WriteLine("[S-DOS] Rebooting in 1 seconds...");
                    System.Threading.Thread.Sleep(1000);
                    System.Console.WriteLine("");
                    System.Console.WriteLine("[S-DOS] Rebooting...");
                    System.Threading.Thread.Sleep(800);
                    Cosmos.System.PCSpeaker.Beep(1500, 1000);
                    Power.Reboot();
                    break;

                case "deviceinfo":
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("[S-DOS] System Info:");
                    System.Console.WriteLine("");
                    string CPUNAME = Cosmos.Core.CPU.GetCPUBrandString();
                    string CPUVENDNAME = Cosmos.Core.CPU.GetCPUVendorName();
                    uint AMMOUNTRAM = Cosmos.Core.CPU.GetAmountOfRAM();
                    ulong USEDRAM = Cosmos.Core.CPU.GetAmountOfRAM() - Cosmos.Core.GCImplementation.GetAvailableRAM();
                    ulong CPUUPTIME = Cosmos.Core.CPU.GetCPUUptime();

                    System.Console.WriteLine(@"CPU: {0}
CPU Vendor: {1}
RAM: {2} MB
Used RAM: {3} MB
CPU Uptime: {4}", CPUNAME, CPUVENDNAME, AMMOUNTRAM, USEDRAM, CPUUPTIME);
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    break;

                case "echo":
                    System.Console.Write("[S-DOS] echo: ");
                    string echotype = System.Console.ReadLine();
                    System.Console.WriteLine("[S-DOS] echo >> " + echotype);
                    break;

                case "version":
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    System.Console.WriteLine("  STell-DOS " + Kernel.Version);
                    System.Console.WriteLine("");
                    System.Console.WriteLine("");
                    break;
            }
        }
    }
}
