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

        static List<string> SessionPaths = new List<string>();
        static List<Sample> SessionSamples = new List<Sample>();

        static void Main(string[] args)
        {
            Sample.ParseType(@"D:\AbletonProjects\Samples\Processed\Consolidate\koko.nikita.is.moskva.oneLove.kick.test.wav");
            Console.WriteLine("ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd");

            DawWatcher dawWatcher = new DawWatcher(DAW_NAME);
            dawWatcher.Subscribe(gg);
            /*
            ProjectWatcher project = new ProjectWatcher(@"D:\AbletonProjects\TESTPROJ_2.als");
            project.Subscribe(gg2);
            */
            SampleWatcher sampleWatcher = new SampleWatcher(CONSOLIDATE_PATH, "*.wav");
            sampleWatcher.Subscribe(gg3);

            while (true) Thread.Sleep(100);
        }

        public static void gg(object sender, DawWatcher.DawState incomingState)
        {
            Console.WriteLine($"[{sender}] \t-> State changed to {incomingState}");

            SessionSamples = ListController.ConvertToSampleList(SessionPaths);

            if(incomingState == DawWatcher.DawState.Closed)
            {
                foreach (Sample item in SessionSamples)
                {
                    Console.WriteLine($"{item.Name} -> {item.FullPath}");
                }
            }
        }

        public static void gg2(string sender, string path, ProjectWatcher.CheckStatus status)
        {
            Console.WriteLine($"[{sender}] \t-> \"{path}\" - {status}");
        }

        public static void gg3(string sender, string fullPath, WatcherChangeTypes changeType)
        {
            ListController.AddSample(fullPath, SessionPaths);
        }
    }
}
