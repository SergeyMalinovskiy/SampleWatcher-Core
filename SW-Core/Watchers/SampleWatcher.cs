using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace SW_Core.Watchers
{
    class SampleWatcher
    {
        FileSystemWatcher watcher = new FileSystemWatcher();

        private string DirPath = "";
        private string FileFormat = "*.*";

        private string LastSampleName = "";

        private event Action<string, string, WatcherChangeTypes> OnSamplesChanged;
        public SampleWatcher(string watchDirectory, string fileFormat)
        {
            if (!Directory.Exists(watchDirectory)) throw new DirectoryNotFoundException();

            DirPath = watchDirectory;
            FileFormat = fileFormat != "" ? fileFormat : FileFormat;

            Watch();
        }

        public void Subscribe(Action<string, string, WatcherChangeTypes> func)
        {
            OnSamplesChanged += func;
        }

        public void Unsubscribe(Action<string, string, WatcherChangeTypes> func)
        {
            OnSamplesChanged -= func;
        }

        private void Watch()
        {
            watcher.Path = DirPath;

            watcher.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;


            watcher.Filter = FileFormat;

            watcher.Changed += OnChanged;

            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            
            if(LastSampleName != e.Name)
            {
                Console.WriteLine($"{e.Name} {e.ChangeType}");
                OnSamplesChanged?.Invoke(this.GetHashCode().ToString(), e.FullPath, e.ChangeType);
                LastSampleName = e.Name;
            }
           
        }
    }
}
