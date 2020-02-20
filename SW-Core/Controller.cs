using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

using SW_Core.Watchers;
using SW_Core.Handlers;
using SW_Core.Entities;

namespace SW_Core
{
    class Controller
    {
        const string DAW_NAME = "ableton";
        const string CONSOLIDATE_PATH   = @"D:\AbletonProjects\Samples\Processed\Consolidate";
        const string RECORD_PATH        = @"D:\AbletonProjects\Samples\Recorded";

        const string PROJECT_FILE_PATH  = @"D:\AbletonProjects\TESTPROJ_2.als";
        const string DETECT_FILE_FORMAT = "*.wav";

        const string PACK_DIRECTORY = @"D:\AbletonProjects\";
        const string PACK_NAME = "MyAnotherPaxck";

        static Packer packer;

        static void Main(string[] args)
        {
            packer = new Packer(PACK_DIRECTORY, PACK_NAME);

            DawWatcher DAW = new DawWatcher(DAW_NAME);
            ProjectWatcher Project = new ProjectWatcher(PROJECT_FILE_PATH);
            SampleWatcher sampleWatcher = new SampleWatcher(CONSOLIDATE_PATH, DETECT_FILE_FORMAT);

            DAW.Subscribe(OnChangeDAWProcessState);            
            Project.Subscribe(OnSaveProjectFile);
            sampleWatcher.Subscribe(OnIncomingFileChange);

            while (true) Thread.Sleep(100);
        }

        public static void OnChangeDAWProcessState(object sender, DawWatcher.DawState incomingState)
        {
            Console.WriteLine($"[{sender}] \t-> State changed to {incomingState}");

            switch (incomingState)
            {
                case DawWatcher.DawState.Closed:
                    packer.FillPack(ListController.SESSION_LIST);
                    break;
                default:
                    break;
            }
        }

        public static void OnSaveProjectFile(string sender, string path, ProjectWatcher.CheckStatus status)
        {
            Console.WriteLine($"[{sender}] \t-> \"{path}\" - {status}");

            ListController.ShowSessionList();
        }

        public static void OnIncomingFileChange(string sender, string fullPath, WatcherChangeTypes changeType)
        {
            ListController.AddToSession(fullPath);
        }
    }
}
