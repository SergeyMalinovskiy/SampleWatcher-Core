using SW_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace SW_Core.Watchers
{
    using DIR = System.IO.Directory;
    class Packer
    {
        const bool OVERWRITE_FILES = true;

        private string directory = "";
        public string Name { get; private set; }
        public string Directory
        {
            get => directory;
            private set
            {
                DIR.CreateDirectory(value);

                directory = value;
            }
        }

        public Packer(string fullDestinationDirectory)
        {
            Directory = fullDestinationDirectory;
        }

        public Packer(string destinationDirectory, string packName)
        {
            Name = packName != ""
                ? packName.Trim('\\')
                : throw new ArgumentNullException();

            Directory = destinationDirectory.TrimEnd('\\') + '\\' + packName;
        }

        public void FillPack(List<Sample> sampleList)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("COPY...");

            foreach(Sample sample in sampleList)
            {
                string srcPath  = sample.FullPath;
                string destDir  = @$"{Directory}\{sample.Type}";
                string destPath = @$"{destDir}\{sample.Type}_{sample.Name}{sample.FileExtension}";

                DIR.CreateDirectory(destDir);
                File.Copy(sample.FullPath, destPath, OVERWRITE_FILES);

                Console.WriteLine($"from {srcPath} \t\t-> {destPath}");
            }
            Console.WriteLine("\nDONE!\n");
            Console.ResetColor();
        }
    }
}
