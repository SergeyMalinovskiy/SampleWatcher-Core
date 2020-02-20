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

        private WorkStations workStation;

        private event Action<string, string, WatcherChangeTypes, WorkStations> OnSamplesChanged;
        public SampleWatcher(string watchDirectory, string fileFormat, WorkStations ws)
        {
            if (!Directory.Exists(watchDirectory)) throw new DirectoryNotFoundException();

            DirPath = watchDirectory;
            FileFormat = fileFormat != "" ? fileFormat : FileFormat;

            workStation = ws;

            Watch();
        }

        public void Subscribe(Action<string, string, WatcherChangeTypes, WorkStations> func)
        {
            OnSamplesChanged += func;
        }

        public void Unsubscribe(Action<string, string, WatcherChangeTypes, WorkStations> func)
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
                OnSamplesChanged?.Invoke(this.GetHashCode().ToString(), e.FullPath, e.ChangeType, workStation);
                LastSampleName = e.Name;
            }
           
        }
    }
}
