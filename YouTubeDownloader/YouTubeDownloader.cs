using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    class YouTubeDownloader
    {
        public void Download(VideoInfo info)
        {
            bool answer = UserInterface.IsVideo();
            if (answer)
            {
                DownloadVideo(info);
            }
            else
            {
                DownloadAudio(info);
            }
        }

        private void DownloadAudio(VideoInfo video)
        {
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }


            string windowsUserName = Environment.UserName;
            var videoDownloader = new VideoDownloader(video, Path.Combine(@"C:\Users\" + windowsUserName + @"\downloads", video.Title + video.VideoExtension));

            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
            videoDownloader.Execute();
        }

        private void DownloadVideo(VideoInfo video)
        {
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }


            string windowsUserName = Environment.UserName;
            var videoPath = @"C:\Users\" + windowsUserName + @"\downloads";
            var videoDownloader = new VideoDownloader(video, Path.Combine(videoPath, video.Title + video.VideoExtension));


            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            videoDownloader.Execute();


            VideoToAudioConverter converter = new VideoToAudioConverter(videoPath, video.Title, video.VideoExtension);
            converter.ConvertVideoToAudioFile();
        }
    }
}
