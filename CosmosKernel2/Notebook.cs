using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;
using CosmosKernel2;

namespace CosmosKernel2
{

    public class Nano
    {
        string FullName = "Notebook Text Editor v1.0";
        char[] line = new char[80]; int pointer = 0;
        List<string> lines = new List<string>();
        string[] final;
        public void initNano(string path)
        {
            Console.Clear();
            drawTopBar();
            Console.SetCursorPosition(0, 1);
            ConsoleKeyInfo c; cleanArray(line);
            while ((c = Console.ReadKey(true)) != null)
            {
                drawTopBar();
                char ch = c.KeyChar;
                if (c.Key == ConsoleKey.Escape)
                    break;
                else if ((c.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    if (c.Key == ConsoleKey.X)
                    {
                        lines.Add(new string(line).TrimEnd()); //Add any unadded lines
                        listCheck(); final = lines.ToArray(); //Store vars
                        string foo = concatString(final); //Get the final text
                        Console.WriteLine("Here comes the concated text: \n\n" + foo);
                        File.WriteAllText(path, foo); //Write to file
                        Console.ReadKey();
                        break;
                    }
                }

                switch (c.Key)
                {
                    case ConsoleKey.Home: break;
                    case ConsoleKey.PageUp: break;
                    case ConsoleKey.PageDown: break;
                    case ConsoleKey.End: break;
                    case ConsoleKey.UpArrow: break;
                    case ConsoleKey.DownArrow: break;
                    case ConsoleKey.LeftArrow: if (pointer > 0) { pointer--; Console.CursorLeft--; } break;
                    case ConsoleKey.RightArrow: if (pointer < 80) { pointer++; Console.CursorLeft++; if (line[pointer] == 0) line[pointer] = ' '; } break;
                    case ConsoleKey.Backspace: deleteChar(); break;
                    case ConsoleKey.Delete: deleteChar(); break;
                    case ConsoleKey.Enter:
                        lines.Add(new string(line).TrimEnd()); cleanArray(line); Console.CursorLeft = 0; Console.CursorTop++; pointer = 0;
                        break;
                    default: line[pointer] = ch; pointer++; Console.Write(ch); break;
                }
            }
            Kernel.ClearConsole();
            System.Console.WriteLine("[S-DOS] The text file \"" + path + "\" has been saved.");
        }

        private string concatString(string[] s)
        {
            string t = "";
            if (s.Length >= 1)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    Kernel.PrintDebug("Lines: " + s.Length);
                    if (!string.IsNullOrWhiteSpace(s[i]))
                        t = string.Concat(t, s[i].TrimEnd(), Environment.NewLine);
                }
            }
            else
                t = s[0];
            t = string.Concat(t, '\0');
            Kernel.PrintDebug("Concat:\n" + t);
            return t;
        }

        private void cleanArray(char[] c)
        {
            for (int i = 0; i < c.Length; i++)
                c[i] = ' ';
        }

        private void drawTopBar()
        {
            int x = Console.CursorLeft, y = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(FullName+"                               ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[CTRL+X]Save   [ESC]Exit\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(x, y);
        }

        //Decrease the pointer, decrease the cusor pos by 1, write whitespace, decrease
        //cursor pos by 1 again, replace char in buffer with whitespace
        private void deleteChar()
        {
            if ((Console.CursorLeft >= 1) && (pointer >= 1))
            {
                pointer--; Console.CursorLeft--;
                Console.Write(" "); Console.CursorLeft--;
                line[pointer] = ' ';
            }
        }

        private void listCheck()
        {
            foreach (var s in lines)
                Kernel.PrintDebug(" List: " + s + "\n"); //Works!
        }

        private string[] arrayCheck(string[] s)
        {
            foreach (var ss in s)
            {
                Kernel.PrintDebug(" Line: " + ss + "\n"); //Works!
            }
            return s; //Just return the input...
        }
    }
}