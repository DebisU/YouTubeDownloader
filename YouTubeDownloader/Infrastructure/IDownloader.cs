using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader.Infrastructure
{
    public interface IDownloader
    {
        void Download(VideoInfo video);
        void DownloadAsync(VideoInfo video);
        IEnumerable<string> RetrieveDownloadOptionsAsString(string youtubeUrl);
        Task<IEnumerable<VideoInfo>> RetrieveDownloadOptions(string youtubeUrl);
    }
}
