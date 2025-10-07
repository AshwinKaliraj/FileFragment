using System;
using System.IO;
using System.Linq;

namespace FileFragmentation
{
    public class FileController
    {
        private readonly string basePath = @"C:\Users\ashwin.kaliraj\OneDrive - psiog.com\Desktop\FileFragmentation\FileFragmentation\ProjectFiles";

        public void StartProcess()
        {
            
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

             DeleteAllFiles();

            Console.Clear();
  

            string inputFilePath = Path.Combine(basePath, "input.txt");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter your paragraph (type 'end' in a new line to finish):");
            Console.ResetColor();
            string paragraph = "";
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                if (line.Trim().ToLower() == "end")
                    break;
                paragraph += line;
            }

          
            File.WriteAllText(inputFilePath, paragraph);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nInput saved successfully to: input.txt\n");
            Console.ResetColor();

           
            Console.Write("Enter number of characters per fragment: ");
            int fragmentSize = int.Parse(Console.ReadLine());

            FragmentFile(inputFilePath, fragmentSize);

           
            Menu();
        }

        private void DeleteAllFiles()
        {
            if (Directory.Exists(basePath))
            {
                foreach (string file in Directory.GetFiles(basePath))
                {
                    File.Delete(file);
                }
            }
        }

        private void Menu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. View Fragment");
                Console.WriteLine("2. Defragment (Create output.txt)");
                Console.WriteLine("3. Exit");
                Console.ResetColor();
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewFragment();
                        break;
                    case "2":
                        DefragmentFiles();
                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Exiting program...");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void FragmentFile(string inputFile, int fragmentSize)
        {
            string text = File.ReadAllText(inputFile);
            string[] fragments = SplitText(text, fragmentSize);

            int totalFiles = fragments.Length;
            int digits = totalFiles.ToString().Length;

            for (int i = 0; i < totalFiles; i++)
            {
                string fileName = (i + 1).ToString().PadLeft(digits, '0') + ".txt";
                string fragmentPath = Path.Combine(basePath, fileName);
                File.WriteAllText(fragmentPath, fragments[i]);
            }

          
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Files Created ---");
            var files = Directory.GetFiles(basePath, "*.txt")
                                 .Where(f => !f.EndsWith("input.txt") && !f.EndsWith("output.txt"))
                                 .OrderBy(f => f)
                                 .ToList();

            foreach (string file in files)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Path.GetFileName(file));
            }
            Console.ResetColor();
            Console.WriteLine("----------------------\n");
        }

        private string[] SplitText(string text, int size)
        {
            return Enumerable.Range(0, (int)Math.Ceiling(text.Length / (double)size))
                             .Select(i => text.Substring(i * size, Math.Min(size, text.Length - i * size)))
                             .ToArray();
        }

        private void ViewFragment()
        {
         
            var files = Directory.GetFiles(basePath, "*.txt")
                                 .Where(f => !f.EndsWith("input.txt") && !f.EndsWith("output.txt"))
                                 .OrderBy(f => f)
                                 .ToList();

            if (files.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo fragment files available!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n--- Available Fragments ---");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
            Console.WriteLine("---------------------------\n");

            Console.Write("Enter the fragment file name exactly (e.g., 001.txt): ");
            string fileName = Console.ReadLine()?.Trim();
            string filePath = Path.Combine(basePath, fileName);

            if (File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nContents of {fileName}:\n");
                Console.ResetColor();
                Console.WriteLine(File.ReadAllText(filePath));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n File not found! Make sure you enter the exact filename.");
                Console.ResetColor();
            }
        }

        private void DefragmentFiles()
        {
            var fragmentFiles = Directory.GetFiles(basePath, "*.txt")
                                         .Where(f => !f.EndsWith("input.txt") && !f.EndsWith("output.txt"))
                                         .OrderBy(f => f)
                                         .ToList();

            if (fragmentFiles.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo fragment files found to defragment!");
                Console.ResetColor();
                return;
            }

            string combinedText = "";
            foreach (string file in fragmentFiles)
            {
                combinedText += File.ReadAllText(file);
            }

            string outputFile = Path.Combine(basePath, "output.txt");
            File.WriteAllText(outputFile, combinedText);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nDefragmentation complete! Output file created: output.txt\n");
            Console.ResetColor();

            string inputFile = Path.Combine(basePath, "input.txt");
            if (File.ReadAllText(inputFile) == File.ReadAllText(outputFile))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" SUCCESS: Input and Output files match perfectly!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" ERROR: Files do not match. Something went wrong.");
                Console.ResetColor();
            }

            Console.Write("\nDo you want to repeat the whole process? (y/n): ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "y")
            {
                Console.Clear();
                StartProcess();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting program...");
                Console.ResetColor();
                Environment.Exit(0);
            }
        }
    }
}
