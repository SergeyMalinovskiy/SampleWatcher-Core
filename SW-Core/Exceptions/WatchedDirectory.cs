using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SW_Core.Exceptions
{
    class WatchedDirectoryNotFoundException:DirectoryNotFoundException
    {
        public WatchedDirectoryNotFoundException(string dir):base(message:$"Watched directory \"{dir}\" not found!")
        {
            
        }
    }
}
