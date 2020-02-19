using System;
using System.Diagnostics;
using System.Timers;

namespace SW_Core.Watchers
{
    class DawWatcher
    {
        private Process DawProcess;
        private string  DawName;

        public enum DawState
        {
            NotFound = 0x00,
            Launched = 0x01,
            Closed   = 0x02
        }

        private DawState currState = DawState.NotFound;
        private DawState prevState = DawState.NotFound;

        public event Action<object, DawState> OnDawStateChanged;

        private Timer CheckTimer;

        public DawWatcher(string name, uint checkDelay = 1000)
        {
            DawName = name;

            CheckTimer = new Timer(checkDelay);
            CheckTimer.Elapsed += Check;

            CheckTimer.Start();
        }

        public void Subscribe(Action<object, DawState> func)
        {
            OnDawStateChanged += func;
        }

        public void Unsubscribe(Action<object, DawState> func)
        {
            OnDawStateChanged -= func;
        }

        private void Check(object sender, EventArgs e)
        {
            DawProcess = GetDawProcess(DawName);

            if(currState != prevState)
            {
                OnDawStateChanged?.Invoke(DawName, currState);
                prevState = currState;
            }
        }

        private void DawClosingHandler(object sender, EventArgs e)
        {
            currState = DawState.Closed;
        }

        private Process GetDawProcess(string name)
        {
            // TODO: Find a safer solution for watching processes, for possible collisions reason -> Example: ableton.exe and ableton-util.exe
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.ToLower().Contains(name.ToLower()))
                {
                    proc.Exited += DawClosingHandler;
                    proc.EnableRaisingEvents = true;

                    currState = DawState.Launched;
                    return proc;
                }
            }
            return null;
        }

        ~DawWatcher()
        {
            CheckTimer.Stop();

            CheckTimer.Dispose();
            DawProcess.Dispose();
        }

    }
}
