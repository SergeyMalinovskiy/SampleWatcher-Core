using System;
using System.Collections.Generic;
using System.Text;



namespace SW_Core.Watchers
{
    using DIR = System.IO.Directory;
    class Packer
    {
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

            Console.WriteLine("Directory for pack created!");
        }

        public Packer(string destinationDirectory, string packName)
        {
            Name = packName != ""
                ? packName.Trim('\\')
                : throw new ArgumentNullException();

            Directory = destinationDirectory.TrimEnd('\\') + '\\' + packName;

            Console.WriteLine("Directory for pack created!");
        }
    }
}
