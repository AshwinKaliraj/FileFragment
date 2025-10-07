using System;
using System.Collections.Generic;

namespace FileFragmentationApp.View
{
    public class FileView
    {
   
        public string GetParagraphFromUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            Console.WriteLine("           Enter Your Paragraph           ");
            Console.WriteLine(" (Press Enter on empty line to finish)   ");
            Console.ResetColor();

            string paragraph = "";
            string line;
            while (true)
            {
                line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) break;
                paragraph += line;
            }
            return paragraph.TrimEnd();
        }

        public int GetFragmentSizeFromUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEnter number of characters per fragment:");
            Console.ResetColor();

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int size) && size > 0)
                    return size;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input, enter a positive number:");
                Console.ResetColor();
            }
        }

        public string GetFragmentFileNumber()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nEnter fragment file number to view :");
            Console.ResetColor();
            return Console.ReadLine();
        }

        public void ShowMessage(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + msg);
            Console.ResetColor();
        }

        public void ShowContent(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n----- Content -----");
            Console.WriteLine(text);
            Console.WriteLine("------------------\n");
            Console.ResetColor();
        }

        public void ShowFragmentFiles(List<string> fragments)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nFragment files created:");
            foreach (var f in fragments)
            {
                Console.WriteLine(f);
            }
            Console.ResetColor();
        }

        public void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n");
            Console.WriteLine("   MENU    ");
            Console.WriteLine(" 1. View Fragment ");
            Console.WriteLine(" 2. Defragment ");
            Console.WriteLine(" 3. Back / Exit ");
            Console.ResetColor();
        }

        public int GetMenuChoice()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nEnter your choice: ");
            Console.ResetColor();

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
                    return choice;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice! Enter 1, 2 or 3.");
                Console.ResetColor();
            }
        }
    }
}
