using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExtractor;
using YouTubeDownloader.Infrastructure;

namespace YouTubeDownloader.Infrastructure
{
    public class MyYoutubeDownloader : IDownloader
    {
        private ResourceManager rm;
        private string VideoSavePath;
        public List<VideoInfo> videoInfo;

        public MyYoutubeDownloader()
        {
            rm = new ResourceManager("YouTubeDownloader.MyResources", Assembly.GetExecutingAssembly());
            VideoSavePath = GetVideoSavePath();
            videoInfo = new List<VideoInfo>();
        }

        private string GetVideoSavePath()
        {
            return rm.GetString("PathToSave").Replace("windowsUserName", Environment.UserName);
        }

        public void Download(VideoInfo video)
        {
            //bool answer = UserInterface.IsVideo();
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            VideoDownloader videoDownloader = new VideoDownloader(video, Path.Combine(VideoSavePath, video.Title + video.VideoExtension));

            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

            videoDownloader.Execute();

            //if (!answer)
            //{
                VideoToAudioConverter converter = new VideoToAudioConverter(VideoSavePath, video.Title, video.VideoExtension);
                converter.ConvertVideoToAudioFile();
                DeleteFile(VideoSavePath + @"\" +  video.Title + video.VideoExtension);
            //}
        }

        public async void DownloadAsync(VideoInfo video)
        {
            Thread downloadVideoThread = new Thread(() => Download(video));
            downloadVideoThread.Start();
        }

        public IEnumerable<string> RetrieveDownloadOptionsAsString(string youtubeUrl)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl); // seems that this call uses Newtonsoft.Json
            List<string> videosTypeAndResolutionList = new List<string>();
            foreach (var actualVideo in videoInfos)
            {
                videosTypeAndResolutionList.Add(actualVideo.ToString());
            }
            return videosTypeAndResolutionList;
        }

        public async Task<IEnumerable<VideoInfo>> RetrieveDownloadOptionsAsync(string youtubeUrl)
        {
            return await Task.FromResult<IEnumerable<VideoInfo>>(RetrieveDownloadOptions(youtubeUrl));
        }

        public IEnumerable<VideoInfo> RetrieveDownloadOptions(string youtubeUrl)
        {
            return DownloadUrlResolver.GetDownloadUrls(youtubeUrl);
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
