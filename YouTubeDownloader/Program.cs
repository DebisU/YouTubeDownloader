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
            bool isPlayList = UserInterface.IsPlaylist();

            if (!isPlayList)
            {
                string youtubeUrl = UserInterface.AskForYouTubeUrl();
                IEnumerable<VideoInfo> videoInfos = UserInterface.PrintAndRetrieveDownloadOptions(youtubeUrl);
                int selectedIndex = UserInterface.SelectIndex();

                YouTubeDownloader myDownloader = new YouTubeDownloader();

                myDownloader.Download(videoInfos.ElementAt(selectedIndex));
            }
            else
            {
                throw new NotImplementedException("This module is not ready yet, sorry for the inconveniences");

                List<IEnumerable<VideoInfo>> videoInfos = UserInterface.PrintAndRetrieveDownloadOptions();
                int selectedIndex = UserInterface.SelectIndex();

                YouTubeDownloader myDownloader = new YouTubeDownloader();

                foreach (var item in videoInfos)
                {
                    myDownloader.Download(item.ElementAt(selectedIndex));
                }
            }
        }
    }
}
