using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SW_Core.Utils
{
    static class CommandLineInterface
    {
        private static readonly Regex PARAMS_PATTERN        = new Regex(@"(-\w* (\w|\S)*){1,}");
        private static readonly Regex KEY_PARAM_PATTERN     = new Regex(@"(-\w*){1}");
        private static readonly Regex VALUE_PARAM_PATTERN   = new Regex(@"\b(\s)\b");

        private static List<string> ParamsList = new List<string>();

        static Dictionary<string, Action<string>> BindList = new Dictionary<string, Action<string>>();

        public static void Bind(string paramKey, Action<string> action)
        {
            if(!BindList.ContainsKey(paramKey))
            {
                BindList.Add(paramKey, action);
            }
        }

        public static bool GetCommandLineParams(string[] args)
        {
            string allArgs = "";

            foreach (var arg in args)
            {
                allArgs += arg + " ";
            }

            // Console.WriteLine(allArgs);
            // Console.WriteLine(Regex.Replace(allArgs, PARAMS_PATTERN.ToString(), "[...]"));
            string[] Params = Regex.Split(allArgs, PARAMS_PATTERN.ToString());

            foreach(var i in Params)
            {
                if(
                    Regex.IsMatch(i, PARAMS_PATTERN.ToString()) &&
                    !ParamsList.Contains(i)    
                )
                {
                    GetParamKey(i);
                    ParamsList.Add(i);
                }
            }

            return false;
        }

        public static void ApplyParams()
        {
            if(ParamsList.Count == 0 || BindList.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Params Not Found!");

                Console.ResetColor();
                Console.ReadLine();

                Environment.Exit(87);
            }

            foreach (string param in ParamsList)
            {
                string key      = GetParamKey(param);
                string value    = GetParamValue(param);

                if (BindList.ContainsKey(key))
                {
                    BindList[key](value);
                }
            }
        }

        private static string GetParamKey(string param)
        {
            return Regex.Split(param, KEY_PARAM_PATTERN.ToString())[1];
        }

        private static string GetParamValue(string param)
        {
            string m = Regex.Split(param, VALUE_PARAM_PATTERN.ToString())[2];

            /*
            Console.WriteLine("---23232323>"+param);
            foreach (var a in m)
            {
                Console.WriteLine("->>>>>>"+a);
            }*/

            return m;
        }

        private static bool IsParameter(string inp)
        {
            return Regex.IsMatch(inp ,@"-\w+");
        }
    }
}
