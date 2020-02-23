using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SW_Core.Watchers
{
    static class Config
    {
        const string ApplicationName = "SWCore";
        const string DefaultSamplePackName = "DefaultSamplePack";

        private static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string DefaultSamplePackPath = $@"{AppDataPath}\{ApplicationName}\{DefaultSamplePackName}";

        public static string GetDefaultPackPath()
        {
            Directory.CreateDirectory(DefaultSamplePackPath);

            return DefaultSamplePackPath;
        }
    }
}
