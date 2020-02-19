using SW_Core.Entities;
using System.Collections.Generic;
using System.IO;

namespace SW_Core.Handlers
{
    static class ListController
    {
        public static bool AddSample(string sampleFullPath, List<string> list)
        {
            if (!list.Contains(sampleFullPath))
            {
                list.Add(sampleFullPath);
                return true;
            }
            return false;
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
    }
}
