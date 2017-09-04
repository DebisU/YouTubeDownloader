using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string youtubeUrl = UserInterface.AskForYouTubeUrl();
            IEnumerable<VideoInfo> videoInfos = UserInterface.PrintAndRetrieveDownloadOptions(youtubeUrl);
            int selectedIndex = UserInterface.SelectIndex();

            YouTubeDownloader myDownloader = new YouTubeDownloader();

            myDownloader.Download(videoInfos.ElementAt(selectedIndex));
        }
    }
}
