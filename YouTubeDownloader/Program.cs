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
            Console.WriteLine("Paste youtube audio URL: ");
            string youtubeUrl = Console.ReadLine();
            Console.WriteLine();
            IEnumerable<VideoInfo> videoInfos = PrintAndRetrieveDownloadOptions(youtubeUrl);
            Console.WriteLine("Select one of the options: ");
            int selectedIndex = Convert.ToInt32(Console.ReadLine());
            Download(videoInfos.ElementAt(selectedIndex));
        }




        private static IEnumerable<VideoInfo> PrintAndRetrieveDownloadOptions(string youtubeUrl)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl);
            for (int i = 0; i < videoInfos.Count(); i++)
            {
                Console.WriteLine(i+")  "+videoInfos.ElementAt(i));
            }
            return videoInfos;
        }

        private static void Download(VideoInfo info)
        {
            Console.WriteLine("Do you want Video(V) or Audio(A)");
            string answer = Console.ReadLine().ToLower();
            bool isVideo = answer == "video" || answer.StartsWith("v") ? true : false;
            if (isVideo)
            {
                DownloadVideo(info);
            }
            else
            {
                DownloadAudio(info);
            }
        }

        private static void DownloadVideo(VideoInfo video)
        {
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }
            

            string windowsUserName = Environment.UserName;
            var videoDownloader = new VideoDownloader(video, Path.Combine(@"C:\Users\"+windowsUserName+@"\downloads", video.Title + video.VideoExtension));

            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
            videoDownloader.Execute();
        }


        private static void DownloadAudio(VideoInfo video)
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


            VideoToAudioConverter converter = new VideoToAudioConverter(videoPath,video.Title, video.VideoExtension);
            converter.ConvertVideoToAudioFile();
        }
    }
}
