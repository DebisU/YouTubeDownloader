using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    public class YouTubeDownloader
    {
        private ResourceManager rm;
        private string VideoSavePath;

        public YouTubeDownloader()
        {
            rm = new ResourceManager("YouTubeDownloader.MyResources", Assembly.GetExecutingAssembly());
            VideoSavePath = GetVideoSavePath();
        }

        private string GetVideoSavePath()
        {
            return rm.GetString("PathToSave").Replace("windowsUserName", Environment.UserName);
        }

        public void Download(VideoInfo video)
        {
            bool answer = UserInterface.IsVideo();
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            var videoDownloader = new VideoDownloader(video, Path.Combine(VideoSavePath, video.Title + video.VideoExtension));

            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            videoDownloader.Execute();

            if (!answer)
            {
                VideoToAudioConverter converter = new VideoToAudioConverter(VideoSavePath, video.Title, video.VideoExtension);
                converter.ConvertVideoToAudioFile();
                DeleteFile(VideoSavePath + @"\" +  video.Title + video.VideoExtension);
            }


        }

        private void DeleteFile(string pathWithFile)
        {
            if (File.Exists(pathWithFile))
            {
                File.Delete(pathWithFile);
            }
        }
    }
}
