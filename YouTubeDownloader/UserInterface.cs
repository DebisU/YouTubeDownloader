using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader
{
    public static class UserInterface
    {
        public static bool IsPlaylist()
        {
            Console.WriteLine("Want to paste youtube url or download(D) from the playlist(P) file?");
            return Console.ReadLine().ToLower() == "p" ? true : false;
        }

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

        public static List<IEnumerable<VideoInfo>> RetrieveDownloadOptionsFromPlayList()
        {
            List<IEnumerable<VideoInfo>> videosInfo = new List<IEnumerable<VideoInfo>>();
            List<string> urls = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader("TestFile.txt"))
                {
                    String line = sr.ReadToEnd();
                    urls.Add(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            if (urls.Count > 0 &&  urls != null)
            {
                foreach (var item in urls)
                {
                    videosInfo.Add(DownloadUrlResolver.GetDownloadUrls(item));
                    //IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(item);
                    //for (int i = 0; i < videoInfos.Count(); i++)
                    //{
                    //    Console.WriteLine(i + ")  " + videoInfos.ElementAt(i));
                    //}
                }
 
            }

            return videosInfo;
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
