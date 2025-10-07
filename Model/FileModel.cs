using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileFragmentationApp.Model
{
    public class FileModel
    {
      
        private readonly string basePath = @"C:\Users\ashwin.kaliraj\OneDrive - psiog.com\Desktop\FileFragmentation\FileFragmentation\ProjectFiles";
        private string projectDir;

        public FileModel()
        {
            projectDir = Path.Combine(basePath, "ProjectFiles");
            Directory.CreateDirectory(projectDir);
        }

       
        public void DeleteAllFiles()
        {
            if (Directory.Exists(projectDir))
            {
                foreach (var file in Directory.GetFiles(projectDir))
                {
                    File.Delete(file);
                }
            }
        }

       
        public void SaveInput(string paragraph)
        {
            string inputPath = Path.Combine(projectDir, "input.txt");
            File.WriteAllText(inputPath, paragraph);
        }

       
        public List<string> FragmentInput(int size)
        {
            List<string> fragmentPaths = new List<string>();
            string inputPath = Path.Combine(projectDir, "input.txt");

            if (!File.Exists(inputPath))
                throw new FileNotFoundException("Input file not found.");

            string content = File.ReadAllText(inputPath);
            int totalFragments = (int)Math.Ceiling((double)content.Length / size);

           
            int padWidth = totalFragments.ToString().Length;

            for (int i = 0; i < totalFragments; i++)
            {
                string fragment = content.Substring(i * size, Math.Min(size, content.Length - (i * size)));
                string fileName = $"{(i + 1).ToString().PadLeft(padWidth, '0')}.txt";
                string filePath = Path.Combine(projectDir, fileName);
                File.WriteAllText(filePath, fragment);
                fragmentPaths.Add(filePath);
            }

            return fragmentPaths;
        }

      
        public string ReadFragment(string fileNumber)
        {
            string[] files = Directory.GetFiles(projectDir, "*.txt");
            string filePath = files.FirstOrDefault(f => Path.GetFileNameWithoutExtension(f) == fileNumber);

            if (filePath == null)
                return null;

            return File.ReadAllText(filePath);
        }

 
        public string Defragment(List<string> fragments)
        {
            string outputPath = Path.Combine(projectDir, "output.txt");

         
            fragments = fragments.OrderBy(f => f).ToList();

            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                foreach (var file in fragments)
                {
                    string data = File.ReadAllText(file);
                    sw.Write(data);
                }
            }

            return outputPath;
        }

        
        public bool CompareFiles()
        {
            string inputPath = Path.Combine(projectDir, "input.txt");
            string outputPath = Path.Combine(projectDir, "output.txt");

            if (!File.Exists(inputPath) || !File.Exists(outputPath))
                return false;

            string inputData = File.ReadAllText(inputPath);
            string outputData = File.ReadAllText(outputPath);

            return inputData == outputData;
        }
    }
}
