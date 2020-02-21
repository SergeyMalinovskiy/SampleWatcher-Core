//#define DEBUG

using System;
using System.IO;
using System.Threading;

using SW_Core.Watchers;
using SW_Core.Handlers;
using System.Text.RegularExpressions;
using SW_Core.Utils;
using System.Collections.Generic;

namespace SW_Core
{
    using CLI = CommandLineInterface;
    enum WorkStations
    {
        Ableton,
        FlStudio,
        Cubase,
        Reaper
    }
    class Controller
    {
        private static string DAW_NAME              = "";
        private static string CONSOLIDATE_PATH      = "";
        private static string PACK_DIRECTORY        = "";
        private static string DETECT_FILE_FORMAT    = "";

        static Packer packer;

        static void Init(string[] args)
        {
            CoreLogger.ShowLogo();

            CLI.Bind("-w", (string paramValue) => DAW_NAME              = paramValue);
            CLI.Bind("-s", (string paramValue) => CONSOLIDATE_PATH      = paramValue);
            CLI.Bind("-d", (string paramValue) => PACK_DIRECTORY        = paramValue);
            CLI.Bind("-f", (string paramValue) => DETECT_FILE_FORMAT    = paramValue);

            CLI.GetCommandLineParams(args);
            CLI.ApplyParams();

#if DEBUG
            Console.WriteLine("params");
            foreach (var i in args)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
#endif
        }

        static void Main(string[] args)
        {
            Init(args);

            packer = new Packer(PACK_DIRECTORY);

            DawWatcher DAW = new DawWatcher(DAW_NAME);
            // ProjectWatcher Project = new ProjectWatcher(PROJECT_FILE_PATH);
            SampleWatcher sampleWatcher = new SampleWatcher(CONSOLIDATE_PATH, DETECT_FILE_FORMAT, WorkStations.Ableton);

            DAW.Subscribe(OnChangeDAWProcessState);            
            // Project.Subscribe(OnSaveProjectFile);
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

        public static void OnIncomingFileChange(string sender, string fullPath, WatcherChangeTypes changeType, WorkStations ws)
        {
            string fileName = Path.GetFileNameWithoutExtension(fullPath);

            // Orders for each WorkStation
            switch (ws)
            {
                case WorkStations.Ableton:
                    if (Regex.IsMatch(fileName, @"(-\d){1,}", RegexOptions.IgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"- {fileName} aborted! Invalid name format for {WorkStations.Ableton}!");
                        return;
                    }
                    break;
                default:
                    break;
            }

            if (ListController.AddToSession(fullPath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"* {fileName} was added!");
            } else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"* {fileName} already exists!");
            }
            Console.ResetColor();
        }
    }
}
