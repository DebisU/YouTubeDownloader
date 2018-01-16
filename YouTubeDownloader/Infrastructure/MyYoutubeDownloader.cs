using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader.Infrastructure
{
    public class MyYoutubeDownloader : IDownloader
    {
        private string VideoSavePath;
        public List<VideoInfo> videoInfo;

        public MyYoutubeDownloader()
        {
            VideoSavePath = Constants.DownloadPath;
            videoInfo = new List<VideoInfo>();
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

            /// TODO: Reimplement the audio to video converter.
            //if (!answer)
            //{
                VideoToAudioConverter converter = new VideoToAudioConverter(VideoSavePath, video.Title, video.VideoExtension);
                converter.ConvertVideoToAudioFile();
                DeleteFile(VideoSavePath + @"\" +  video.Title + video.VideoExtension);
            //}
        }

        public void DownloadAsync(VideoInfo video)
        {
            //Thread downloadVideoThread = new Thread(() => Download(video));
            Task downloadVideoTask = new Task(()=> Download(video));
            downloadVideoTask.Start();
            //downloadVideoThread.Start();
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
