using SW_Core.Entities;
using System.Collections.Generic;
using System.IO;
using System;

namespace SW_Core.Handlers
{
    static class ListController
    {
        public static List<Sample> SESSION_LIST { get; private set; }
        public static List<Sample> GLOBAL_LIST  { get; private set; }

        static ListController()
        {
            SESSION_LIST = new List<Sample>();
            GLOBAL_LIST  = new List<Sample>();
        }

        public static bool AddToSession(string sampleFullPath)
        {
            Sample sample = new Sample(sampleFullPath);

            if (!SESSION_LIST.Contains(sample))
            {
                SESSION_LIST.Add(sample);
                return true;
            }

            return false;
        }

        public static bool AddToSession(Sample sample)
        {
            if (!SESSION_LIST.Contains(sample))
            {
                SESSION_LIST.Add(sample);
                return true;
            }

            return false;
        }

        public static bool AddToGlobal(Sample sample)
        {
            if (!GLOBAL_LIST.Contains(sample))
            {
                GLOBAL_LIST.Add(sample);
                return true;
            }
            
            return false;
        }

        public static bool AddSessionToGlobal()
        {
            bool result = false;
            foreach(var sample in SESSION_LIST)
            {
                result |= AddToGlobal(sample);
            }

            return result;
        }

        public static void ShowSessionList()
        {
            if (SESSION_LIST.Count == 0) return;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n =============== SESSION LIST ===============\n");
            foreach (var sample in SESSION_LIST)
            {
                Console.WriteLine($" * {sample.FullPath} | {sample.Name}");
            }

            Console.WriteLine("\n ============================================\n");
            Console.ResetColor();
        }

        public static void ShowGlobalList()
        {

            if (GLOBAL_LIST.Count == 0) return;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n =============== GLOBAL  LIST ===============\n");
            foreach (var sample in GLOBAL_LIST)
            {
                Console.WriteLine($" * {sample.FullPath} | {sample.Name}");
            }

            Console.WriteLine("\n ============================================\n");
            Console.ResetColor();
        }

        public static List<Sample> ConvertToSampleList(List<string> list)
        {
            List<Sample> newList = new List<Sample>();

            foreach(string item in list)
            {
                if(File.Exists(item))
                {
                    newList.Add(new Sample(item));
                }
            }

            return newList;
        }

        public static bool LoadGlobal()
        {
            return true;
        }

        // public static List
    }
}
