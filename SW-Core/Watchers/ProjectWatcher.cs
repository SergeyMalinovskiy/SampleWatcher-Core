using System;
using System.IO;
using System.Timers;

namespace SW_Core.Watchers
{
    class ProjectWatcher
    {
        private string FullPath = "";
        private Timer CheckTimer;

        private DateTime currWriteTime;
        private DateTime lastWriteTime;

        public enum CheckStatus
        {
            NotFound = 0x00,
            Modified = 0x01
        }

        private event Action<string, string, CheckStatus> OnProjectFileSaving;

        private bool IsFirstCheck = true;

        public ProjectWatcher(string fullProjectFilePath, uint checkDelay = 1000)
        {
            if (!File.Exists(fullProjectFilePath)) throw new FileNotFoundException();

            FullPath = fullProjectFilePath;

            CheckTimer = new Timer(checkDelay);
            CheckTimer.Elapsed += Check;
            CheckTimer.Start();

            Console.WriteLine($"Watching at {FullPath}");
        }

        public void Subscribe(Action<string, string, CheckStatus> func)
        {
            OnProjectFileSaving += func;
        }       

        public void Unsubscribe(Action<string, string, CheckStatus> func)
        {
            OnProjectFileSaving -= func;
        }

        private void Check(object sender, EventArgs e)
        {
            if (!File.Exists(FullPath))
            {
                OnProjectFileSaving?.Invoke(
                        this.GetHashCode().ToString(),
                        FullPath, 
                        CheckStatus.NotFound
                    );
            }

            currWriteTime = new FileInfo(FullPath).LastWriteTime;

            if (lastWriteTime < currWriteTime)
            {
                OnProjectFileSaving?.Invoke(
                        this.GetHashCode().ToString(), 
                        FullPath, 
                        CheckStatus.Modified
                    );
                lastWriteTime = currWriteTime;
            }

        }

        ~ProjectWatcher()
        {
            CheckTimer.Stop();
        }
    }
}
