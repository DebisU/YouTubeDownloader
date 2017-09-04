using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    public static class UserInterface
    {
        public static string AskForYouTubeUrl()
        {
            Console.WriteLine("Paste youtube audio URL: ");
            return Console.ReadLine();
        }

        public static IEnumerable<VideoInfo> PrintAndRetrieveDownloadOptions(string youtubeUrl)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl);
            for (int i = 0; i < videoInfos.Count(); i++)
            {
                Console.WriteLine(i + ")  " + videoInfos.ElementAt(i));
            }
            return videoInfos;
        }

        public static int SelectIndex()
        {
            Console.WriteLine("Select index: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static bool IsVideo()
        {
            Console.WriteLine("Do you want Video(V) or Audio(A)");
            string answer = Console.ReadLine().ToLower();
            return answer == "video" || answer.StartsWith("v") ? true : false;
        }
    }
}
