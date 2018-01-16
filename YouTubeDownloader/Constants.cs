using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDownloader
{
    public static class Constants
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager("YouTubeDownloader.MyResources", Assembly.GetExecutingAssembly());
        public static readonly string DownloadPath = ResourceManager.GetString("PathToSave")?.Replace("windowsUserName", Environment.UserName);


    }
}
