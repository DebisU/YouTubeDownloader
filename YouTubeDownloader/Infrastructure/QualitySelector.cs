using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExtractor;

namespace YouTubeDownloader.Infrastructure
{
    public class QualitySelector : IQualitySelector
    {
        public IEnumerable<string> RetrieveDownloadOptionsAsString(string youtubeUrl)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(youtubeUrl); // seems that this call uses Newtonsoft.Json
            List<string> videosTypeAndResolutionList = new List<string>();
            foreach (var actualVideo in videoInfos)
            {
                videosTypeAndResolutionList.Add(actualVideo.Resolution + " - " + actualVideo.VideoType);
                //videosTypeAndResolutionList.Add(actualVideo.ToString());
            }
            return videosTypeAndResolutionList;
        }
    }
}
